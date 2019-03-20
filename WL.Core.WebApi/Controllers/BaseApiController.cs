using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WL.Core.BusinessService;
using WlToolsLib.Expand;
using System.Linq;

namespace WL.Core.WebApi.Controllers
{
    [Produces("application/json")]
    public abstract class BaseApiController : Controller
    {
        //public BaseApiController(IUserSystemBLL userSystemBLL)
        //{
        //    _userSystemBLL = userSystemBLL;
        //}

        //public static ILogger Log4 { get { return logger; } }

        // 日志
        public readonly ILogger<BaseApiController> logger;

        public BaseApiController()
        {

        }

        /// <summary>
        /// 
        /// [FromServices]
        /// </summary>
        /// <param name="log"></param>
        public BaseApiController(ILogger<BaseApiController> log)
        {
            logger = log;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected void LogInfo(string info)
        {
            logger.LogInformation($"{info} --[{DateTime.Now.FullStr()}]");
        }

        #region --单个上传文件 application/octet-stream --

        /// <summary>
        /// 接收单个上传文件 application/octet-stream
        /// </summary>
        /// <param name="filePathFunc"></param>
        /// <returns></returns>
        protected async Task<IActionResult> SingleUpload(Func<HttpRequest, string> filePathFunc)
        {
            try
            {
                if (Request.Headers["Content-Type"].FirstOrDefault() == "application/octet-stream")
                {
                    string targetFilePath = filePathFunc(Request);
                    using (Stream s = new FileStream(targetFilePath, FileMode.OpenOrCreate))
                    {
                        await Request.Body.CopyToAsync(s);
                    }
                }
                return Json("ok");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        #endregion

        #region --上传接收程序，貌似能上传超大文件--

        // Get the default form options so that we can use them to set the default limits for
        // request body data
        // 获取默认配置，可设置请求限制
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        /// <summary>
        /// 这个特性能让这个页面带上验证信息，令牌
        /// 
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //[GenerateAntiforgeryTokenCookieForAjax]
        //public IActionResult Index()
        //{
        //    return View();
        //}




        #region --上传方法, 不自动绑定，可上传大文件--
        /// <summary>
        /// 上传方法
        /// </summary>
        /// <returns></returns>
        // 1. Disable the form value model binding here to take control of handling 
        //    potentially large files.
        //    禁止了表单到模型自动绑定，以便能控制好大文件
        // 2. Typically antiforgery tokens are sent in request body, but since we 
        //    do not want to read the request body early, the tokens are made to be 
        //    sent via headers. The antiforgery token filter first looks for tokens
        //    in the request header and then falls back to reading the body.
        //[HttpPost]
        //[DisableFormValueModelBinding]//不进行表单到模型的绑定
        //[ValidateAntiForgeryToken]//检查令牌
        protected async Task<IActionResult> Upload(Func<IDictionary<string, StringValues>, string> filePathFunc)
        {
            // 检查是否多部分数据类型
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            // Used to accumulate all the form url encoded key value pairs in the request.
            var formAccumulator = new KeyValueAccumulator();
            string targetFilePath = null; //目标文件路经
            //这里应该是获取http提交的分段信息
            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            // 创建一个读取器
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            // 异步读取下一个部件
            var section = await reader.ReadNextSectionAsync();
            // 循环检查读取的部件是否为空
            while (section != null)
            {
                ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);
                // 是否有内容配置头值
                if (hasContentDispositionHeader)
                {
                    // 是否文件内容配置
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        // 一个临时文件名
                        section.Headers.Keys.Foreach(key => {
                            LogInfo($"{key}:{section.Headers[key]}");
                        });
                        var file_ex = section.ContentDisposition.LastIndexOfRight(".").RemoveFileNameIllegalChar();
                        targetFilePath = Constant.CurrDir + "temp"+DateTime.Now.DataStr("")+"." + file_ex;//Path.GetTempFileName();
                        targetFilePath = filePathFunc(section.Headers);
                        // 写入文件到临时文件
                        using (var targetStream = System.IO.File.Create(targetFilePath))
                        { 
                            // 此方法接收数据不全
                            await section.Body.CopyToAsync(targetStream);
                            //LogInfo($"Copied the uploaded file '{targetFilePath}'");
                        }

                    }
                    // 是否有表单，这一段用于处理所有键值对
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {
                        // Content-Disposition: form-data; name="key" value
                        // Do not limit the key name length here because the 
                        // multipart headers length limit is already in effect.
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 5120000,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            // 值长度被MultipartBodyLengthLimit强制限制了
                            var value = await streamReader.ReadToEndAsync();//读取流
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }
                            formAccumulator.Append(key.ToString(), value);
                            LogInfo($"key={key.ToString()} | {value}");

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }
                        }
                    }
                }

                // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                // 读取下一个节点头（部分）
                section = await reader.ReadNextSectionAsync();
            }


            //这里绑定表单数据到
            // Bind form data to a model
            //var user = new User();
            //var formValueProvider = new FormValueProvider(
            //    BindingSource.Form,
            //    new FormCollection(formAccumulator.GetResults()),
            //    CultureInfo.CurrentCulture);

            //var bindingSuccessful = await TryUpdateModelAsync(user, prefix: "",
            //    valueProvider: formValueProvider);
            //if (!bindingSuccessful)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }
            //}

            //var uploadedData = new UploadedData()
            //{
            //    Name = user.Name,
            //    Age = user.Age,
            //    Zipcode = user.Zipcode,
            //    FilePath = targetFilePath
            //};
            return Json("success");
        }
        #endregion

        #region --编码检查获取--
        /// <summary>
        /// 获取编码，大多数是utf-8
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        private static Encoding GetEncoding(MultipartSection section)
        {
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }
        #endregion

        #region --多请求头处理类--
        /// <summary>
        /// 多结构数据请求头
        /// </summary>
        public static class MultipartRequestHelper
        {
            // Content-Type: multipart/form-data; boundary="----WebKitFormBoundarymx2fSWqWSd0OxQqq"
            // The spec says 70 characters is a reasonable limit.
            public static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
            {
                var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).ToString();
                if (string.IsNullOrWhiteSpace(boundary))
                {
                    throw new InvalidDataException("Missing content-type boundary.");
                }

                if (boundary.Length > lengthLimit)
                {
                    throw new InvalidDataException(
                        $"Multipart boundary length limit {lengthLimit} exceeded.");
                }

                return boundary;
            }

            public static bool IsMultipartContentType(string contentType)
            {
                return !string.IsNullOrEmpty(contentType)
                       && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
            }

            public static bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
            {
                // Content-Disposition: form-data; name="key";
                return contentDisposition != null
                       && contentDisposition.DispositionType.Equals("form-data")
                       && StringSegment.IsNullOrEmpty(contentDisposition.FileName)
                       && StringSegment.IsNullOrEmpty(contentDisposition.FileNameStar);
            }

            public static bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
            {
                // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
                return contentDisposition != null
                       && contentDisposition.DispositionType.Equals("form-data")
                       && (!StringSegment.IsNullOrEmpty(contentDisposition.FileName)
                           || !StringSegment.IsNullOrEmpty(contentDisposition.FileNameStar));
            }
        }
        #endregion

        #region --令牌生成 特性--
        /// <summary>
        /// 令牌生成特性，放置在cookie中 上传文件时会检查
        /// </summary>
        public class GenerateAntiforgeryTokenCookieForAjaxAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext context)
            {
                var antiforgery = context.HttpContext.RequestServices.GetService<IAntiforgery>();

                // We can send the request token as a JavaScript-readable cookie, 
                // and Angular will use it by default.
                var tokens = antiforgery.GetAndStoreTokens(context.HttpContext);
                context.HttpContext.Response.Cookies.Append(
                    "XSRF-TOKEN",
                    tokens.RequestToken,
                    new CookieOptions() { HttpOnly = false });
            }
        }
        #endregion

        #region --自动绑定form信息到实体的特性--
        /// <summary>
        /// 自动绑定form信息到实体 的 特性
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
        {
            /// <summary>
            /// 执行中
            /// </summary>
            /// <param name="context"></param>
            public void OnResourceExecuting(ResourceExecutingContext context)
            {
                var factories = context.ValueProviderFactories;
                factories.RemoveType<FormValueProviderFactory>();
                factories.RemoveType<JQueryFormValueProviderFactory>();
            }

            public void OnResourceExecuted(ResourceExecutedContext context)
            {
            }
        }
        #endregion
        #endregion
    }
}