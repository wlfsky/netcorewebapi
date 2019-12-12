using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WL.Core.WebApi.Common;
using WlToolsLib.DataShell;
using WlToolsLib.Extension;

namespace WL.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : BaseApiController
    {
        public ValuesController([FromServices]ILogger<ValuesController> log) : base(log)
        {

        }

        // GET api/values
        [HttpGet]
        [Route("get")]
        public IDataShell<string> Get()
        {
            return "欢迎使用 WL.Core.WebApi 2019".Success(info:"欢迎信息");
        }


        [HttpGet]
        [Route("info")]
        public IDataShell<List<string>> Info()
        {
            var infos = new List<string>();
            infos.Add(Constant.CurrDir);
            infos.Add(Constant.DbConnConfigFile());

            var r = infos.Success();
            return r;
        }

        // POST api/values
        [HttpPost]
        [Route("upfile")]
        [DisableFormValueModelBinding]//不进行表单到模型的绑定
        [RequestSizeLimit(512_000_000)]
        public Task UpFile()
        {
            return Upload(header => {
                var file_ex = header["Content-Disposition"].ToString().LastIndexOfRight(".").RemoveFileNameIllegalChar();
                var filepath = Constant.CurrDir + "temp" + DateTime.Now.DataStr("") + "." + file_ex;
                return filepath;
            });
        }

        [HttpPost]
        [Route("upload")]
        public Task Upload()
        {
            return SingleUpload(req =>
            {
                var fileName = Request.Headers["FileName"].FirstOrDefault();
                var filePath = Constant.CurrDir + "audio\\" + fileName;
                return filePath;
            });
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
