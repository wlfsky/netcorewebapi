<template>
  <div id="app">
    <img src="./assets/logo.png">
    <router-view/>
  </div>
</template>

<script>
import HttpClient from './common/httpclient';
import Configs from '../config/index';

export default {
  name: 'App',
  data(){
    return {

    };
  },
  created(){
    let req = {id:100};
    HttpClient.post('/do/something', req)
    .then((res)=>{

    })
    .catch((err)=>{

    });
  },
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
