import axios, {
    AxiosRequestConfig,
    AxiosResponse,
    AxiosError,
    AxiosInstance,
    AxiosAdapter,
    Cancel,
    CancelToken,
    CancelTokenSource,
    AxiosTransformer,
    Canceler,
} from 'axios';
import Promise from 'promise';
import Vue from 'vue';
import { Result } from '../models/Result';
import { Toast } from 'vant';
import { Dialog } from 'vant';
import Config from '../config';
import lib from '../common/Lib';
// import { stringify } from 'querystring';
import Options from './Options';
import KeyValue from './KeyValue';
import { Component, Prop } from 'vue-property-decorator';

Vue.use(Dialog);
Vue.use(Toast);

export default class HttpClient {
    public static ErrMsg: string = '发生了一个令程序员毛骨悚然的恐怖错误！';
    public static ErrDuration: number = 6 * 1000;
    public static Get<Tin, Tou>(url: string, data: Tin): Promise<Tou> {
        // const loading = Toast.loading({ mask: true, message: '加载中...' });
        lib.log('get 请求 ', data);
        return new Promise<Tou>((resolve, reject) => {
            const hc: HttpClient = new HttpClient();
            hc.config.url = url;
            hc.config.method = 'GET';
            hc.config.data = data;
            let errfn: (e: any) => void = null;
            if (errfn === null) {
                errfn = (e) => {
                    lib.log('Get Err:--', e);
                    const errmsg: string = HttpClient.ErrMsg + `[${e}]`;
                    // Toast({ duration: HttpClient.ErrDuration, message: errmsg });
                    // Toast.fail(errmsg)
                    // Dialog.alert({
                    //   message: errmsg
                    // }).then(() => {
                    //   // on close
                    // })
                };
            }
            axios(hc.config).then((res: AxiosResponse) => {
                lib.log('Get Result :--', res);
                if (res.status === 200) {
                    const result = res.data as Tou;
                    lib.log(typeof result, result);
                    resolve(result);
                    // loading.close();
                    Toast.clear();
                } else {
                    reject(res.data as Tou);
                    // loading.close();
                }
            }).catch((err: AxiosError) => {
                errfn(err);
                // loading.close();
                // Toast.clear()
            });
        });
    }

    public static Post<Tin, Tou>(url: string, data: Tin): Promise<Tou> {
        const loading = Toast.loading({ mask: true, message: '加载中...' });
        return new Promise<Tou>((resolve, reject) => {
            const hc: HttpClient = new HttpClient();
            hc.config.url = url;
            hc.config.method = 'POST';
            hc.config.data = data;
            let errfn: (e: any) => void = null;
            if (errfn == null) {
                errfn = (e) => {
                    console.log('Post Err:--');
                    console.log(e);
                    const errmsg: string = HttpClient.ErrMsg + `[${e}]`;
                    Toast({ duration: HttpClient.ErrDuration, message: errmsg });
                    // Toast.fail(errmsg)
                    // Dialog.alert({
                    //   message: errmsg
                    // }).then(() => {
                    //   // on close
                    // })
                }
            }
            axios(hc.config).then((res: AxiosResponse) => {
                console.log('Post :--');
                console.log(res);
                if (res.status === 200) {
                    resolve(res.data as Tou);
                    loading.close();
                    Toast.clear();
                } else {
                    reject(res.data as Tou);
                    loading.close();
                }
            }).catch((err: AxiosError) => {
                errfn(err);
                loading.close();
            });
        });
    }

    public config: AxiosRequestConfig = {
        url: '/',
        method: 'get',
        baseURL: Config.HttpConfig.BaseUrl,
        transformRequest: undefined,
        transformResponse: [(data) => {
            if (data === null) {
                return new Result(false, '无返回数据', null);
            } else {
                return new Result(data.Success, data.Msg, data.Data);
            }
        }],
        headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' },
        params: {},
        paramsSerializer: undefined,
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
        cancelToken: new axios.CancelToken((cancel: Canceler) => ''),
    };
}
