<template>
  <div id="app">
    <van-nav-bar v-bind:title="computedPageTitle" v-bind:left-text="leftButtonText" v-bind:right-text="rightButtonText"
      v-bind:left-arrow="leftButtonArrowShow"
      @click-left="leftButtonClickHandle"
      @click-right="rightButtonClickHandle"
    />
    <!-- <mt-header fixed v-bind:title="computedPageTitle"></mt-header> -->
    <router-view/>
    <van-tabbar v-model="bottomBarActiveIndex">
      <template v-for="(bbitem, key) in BottomBarItems">
    <van-tabbar-item v-bind:key="key" v-bind:icon="bbitem.BBIcon" v-bind:info="bbitem.BBInfo" @click="bbitem.BBClick()">{{bbitem.BBText}}</van-tabbar-item>
      </template>
    </van-tabbar>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Watch/*, Prop, Emit, Inject, Model, Provide*/ } from 'vue-property-decorator';
// Component组件， Prop上级传来数据，Vue核心， Watch监控
import { NavBar } from 'vant';
import { Tabbar, TabbarItem } from 'vant';
import HttpClient from './common/HttpClient';


Vue.use(NavBar);
Vue.use(Tabbar);
Vue.use(TabbarItem);

@Component({
  components: { }
})
export default class App extends Vue {
  // 顶部按钮
  public appPageTitle: string = '页标题';
  // 左按钮 // 'icon'
  leftButtonType: string = 'text';
  leftButtonText: string = '返回';
  leftButtonIcon: string = '';
  leftButtonArrowShow: boolean = true;
  leftButtonClickHandle: () => void = this.onClickLeft;
  // 右按钮
  rightButtonType: string = 'text';
  rightButtonText: string = '';
  rightButtonIcon: string = '';
  rightButtonClickHandle: () => void = this.onClickRight;
  //
  bottomBarActiveIndex: number = 0;
  BottomBarItems: Array<BottomButton> = new Array<BottomButton>();
  
  // 生命周期钩子
  mounted () {
    this.BottomBarItems.push(new BottomButton(0, 'records', '首页', '', this.goHome));
    this.BottomBarItems.push(new BottomButton(1, 'gold-coin', '关于', '', this.goAbout));
  }

  created () {
    console.log("执行了一次创建！");
    let p = "ok";
    let t = "CF8C652EACD9D7C655DE3246E7BE785EB230C00FE8436B00584A03EFB0D4F8E39975A196FFAA042402CBA53897386D9B178BF0EBA1A5EB7943C8C32F71CF1D4A";
    let resx = HttpClient.TestCallUrl("GET", "http://ocssopenapi.hw.pisendev.com/api/EndUserCenter/DoWork?msg="+p, p, t).then((res) => {
      console.log(res)
    }).catch((err) => {
      console.log(err)
      console.log(err.Message)
    })
  }

  // 计算属性
  public get computedPageTitle () {
    return this.appPageTitle;
  }
  
  public set computedPageTitle (newTitle: string) {
    this.appPageTitle = newTitle;
  }
  // 方法
  disableLeftButton () {
    this.leftButtonText = '';
    this.leftButtonArrowShow = false;
  }
  enableLeftButton () {
    this.leftButtonText = '返回';
    this.leftButtonArrowShow = true;
  }
  goAbout () {
    this.bottomBarActiveIndex = 1;
    this.$router.push({path: '/about'});
  }
  goHome () {
    this.bottomBarActiveIndex = 0;
    this.$router.push({path: '/'});
  }
  // 顶部事件
  onClickLeft () {
    console.log('点击了顶左');
    this.$router.go(-1);
  }
  onClickRight () {
    console.log('点击了顶右');
  }
  // Watch
  @Watch('appPageTitle')
  onAppPageTitleChanged(val: string, oldVal: string) {
    console.log(val);
  }
}

export class BottomButton {
  BBKey: number = 0
  BBIcon: string
  BBText: string
  BBInfo: string
  BBClick: Function
  constructor (key: number, icon: string, text: string, info: string, click_fn: Function) {
    this.BBIcon = icon
    this.BBText = text
    this.BBInfo = info
    this.BBClick = click_fn
  }
}
</script>

<style>
.main_box{}

</style>
