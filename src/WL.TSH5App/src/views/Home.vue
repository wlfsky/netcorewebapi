<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js + TypeScript App"/>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import HelloWorld from '@/components/HelloWorld.vue'; // @ is an alias to /src
import { Toast } from 'vant';
import 'vant/lib/index.css';


@Component({
  components: {
    HelloWorld, Toast,
  },
})
export default class Home extends Vue {
  public navBarPageTitle = '首页';
  public onClickNavBarLeft(): void {
    const x: string = '';
  }

  public onClickNavBarRight(): void {
    const x: string = '';
  }
  created() {
    Toast('提示内容');
  }
}
</script>
