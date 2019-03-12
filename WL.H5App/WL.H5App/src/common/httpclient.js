import axios from 'axios';
import Vue from 'vue';
import Configs from '../config/index';

axios.defaults.baseURL = HttpConfig.BaseUrl;
axios.defaults.timeout = 60000;
export default {
    // get请求
    get(url, param) {
      return new Promise((resolve, reject) => {
        axios({
          method: 'get',
          url,
          params: param
        })
        .then((res)=> {
            console.log(res);
            resolve(res);
        })
        .catch((error) => {
            console.log(error);
            reject(error);
        });
      });
    },
    // post请求
    post(url, param) {
      return new Promise((resolve, reject) => {
        axios({
          method: 'post',
          url,
          data: param,
          headers: {
            'Content-Type': 'application/json'
          }
        })
        .then((res) => {
            console.log(res);
            resolve(res);
        })
        .catch((error) => {
            console.log(error);
            reject(error);
        });
      });
    }
  }