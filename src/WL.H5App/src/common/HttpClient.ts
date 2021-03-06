import axios, {
    AxiosRequestConfig,
    AxiosResponse,
    AxiosError,
    AxiosInstance,
    AxiosAdapter,
    Cancel,
    CancelToken,
    CancelTokenSource,
    Canceler
  } from 'axios'
  import Promise from 'promise'
  import Vue from 'vue'
  import { Result } from '../models/Result'
  import { Toast } from 'vant'
  import { Dialog } from 'vant';
  import Config from '../config';
//   import { stringify } from 'querystring';
  
  Vue.use(Dialog);
  Vue.use(Toast)
  
  export default class HttpClient {
    config: AxiosRequestConfig = {
      url: '/',
      method: 'get',
      baseURL: Config.HttpConfig.BaseUrl,
      transformRequest: null,
      transformResponse: [function (data) {
        if(data === null){
          return new Result(false, '无返回数据', null);
        }
        else{
          return new Result(data.Success, data.Msg, data.Data);
        }
      }],
      headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' },
      params: {},
      paramsSerializer: null,
      data: {},
      timeout: 60 * 1000,
      withCredentials: false,
      // auth: {
      //   username: 'janedoe',
      //   password: 's00pers3cret'
      // },
      responseType: 'json',
      xsrfCookieName: 'XSRF-TOKEN',
      xsrfHeaderName: 'X-XSRF-TOKEN',
      onUploadProgress: (progressEvent: any) => '',
      onDownloadProgress: (progressEvent: any) => '',
      maxContentLength: 6553500,
      validateStatus: (status: number) => status >= 200 && status < 300,
      maxRedirects: 5,
      // proxy: {
      //   host: 'localhost',
      //   port: 52147
      // },
      cancelToken: new axios.CancelToken((cancel: Canceler) => '')
    }
  
    static ErrMsg: string = '发生了一个令程序员毛骨悚然的恐怖错误！'
    static ErrDuration: number = 6 * 1000
  
    static Get<Tin, Tou> (url: string, data: Tin): Promise<Tou> {
      const loading = Toast.loading({ mask: true, message: '加载中...' });
      console.log('get 请求 ');
      console.log(data);
      return new Promise<Tou>(function (resolve, reject) {
        let hc = new HttpClient()
        hc.config.url = url
        hc.config.method = 'GET'
        hc.config.data = data
        hc.config.headers = {'Access-Control-Allow-Origin': '*'};
        let errfn: Function = null
        if (errfn === null) {
          errfn = (e: any) => {
            console.log('Get Err:--')
            console.log(e)
            let errmsg: string = HttpClient.ErrMsg + `[${e}]`
            Toast({duration: HttpClient.ErrDuration, message: errmsg})
            // Toast.fail(errmsg)
            // Dialog.alert({
            //   message: errmsg
            // }).then(() => {
            //   // on close
            // })
          }
        }
        axios(hc.config).then((res: AxiosResponse) => {
          console.log('Get :--')
          console.log(res)
          if (res.status === 200) {
            let result = res.data as Tou;
            console.log(typeof(result));
            console.log(result);
            resolve(result)
            loading.close()
            Toast.clear()
          } else {
            reject(res.data as Tou)
            loading.close()
          }
        }).catch((err: AxiosError) => {
          errfn(err)
          loading.close()
          //Toast.clear()
        })
      })
    }
  
    static Post<Tin, Tou> (url: string, data: Tin): Promise<Tou> {
      const loading = Toast.loading({ mask: true, message: '加载中...' });
      return new Promise<Tou>(function (resolve, reject) {
        let hc = new HttpClient();
        hc.config.url = url;
        hc.config.method = 'POST';
        hc.config.data = data;
        hc.config.headers = {'Access-Control-Allow-Origin': '*'};
        let errfn: Function = null;
        if (errfn == null) {
          errfn = (e: any) => {
            console.log('Post Err:--')
            console.log(e)
            let errmsg: string = HttpClient.ErrMsg + `[${e}]`
            Toast({duration: HttpClient.ErrDuration, message: errmsg})
            // Toast.fail(errmsg)
            // Dialog.alert({
            //   message: errmsg
            // }).then(() => {
            //   // on close
            // })
          }
        }
        axios(hc.config).then((res: AxiosResponse) => {
          console.log('Post :--')
          console.log(res)
          if (res.status === 200) {
            resolve(res.data as Tou)
            loading.close()
            Toast.clear()
          } else {
            reject(res.data as Tou)
            loading.close()
          }
        }).catch((err: AxiosError) => {
          errfn(err)
          loading.close()
        })
      })
    }

    static TestCallUrl(method: 'GET'|'POST', url: string, data: any, token: string): Promise<string> {
      const loading = Toast.loading({ mask: true, message: '加载中...' });
      console.log('get 请求 ');
      console.log(data);
      return new Promise<string>(function (resolve, reject) {
        let hc = new HttpClient()
        hc.config.url = url;
        hc.config.method = method;
        hc.config.data = data;
        hc.config.headers = {'Token': token, 'Access-Control-Allow-Origin': '*'};
        let errfn: Function;
        errfn = (e: any) => {
          console.log('Get Err:--')
          console.log(e)
          let errmsg: string = HttpClient.ErrMsg + `[${e}]`
          Toast({duration: HttpClient.ErrDuration, message: errmsg})
          // Toast.fail(errmsg)
          // Dialog.alert({
          //   message: errmsg
          // }).then(() => {
          //   // on close
          // })
        }
        axios(hc.config).then((res: AxiosResponse) => {
          console.log('Get :--')
          console.log(res)
          if (res.status === 200) {
            let result = res.data as string;
            console.log(typeof(result));
            console.log(result);
            resolve(result)
            loading.close()
            Toast.clear()
          } else {
            reject(res.data as string)
            loading.close()
          }
        }).catch((err: AxiosError) => {
          errfn(err)
          loading.close()
          //Toast.clear()
        })
      })
    }
  }
  
  export class KeyValue {
    Key: string = "";
    Value: string = "";
  }
  
  export class Options {
    method: string = "";
    headers: Array<KeyValue> = new Array<KeyValue>();
    cookies: Array<KeyValue> = new Array<KeyValue>();
  }
  