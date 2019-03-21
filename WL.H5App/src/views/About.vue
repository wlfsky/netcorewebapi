<template>
  <div class="about_sty">
      <img class="" src="../assets/w-logo.png" />
      <div>{{about.WebSite}} <van-icon name="star" /><van-icon name="star" /><van-icon name="star" /></div>
      <div>{{about.WebSiteVersion}}</div>
      <div>{{about.Remark}}</div>
      <div>{{about.LegalInfo}}</div>
      <ul v-for="os in about.OpenSourceInfo" :key="os.OpenSourceName">
          <li>{{os.OpenSourceName}} | {{os.Version}}</li>
      </ul>
      <!-- <router-link to="/sing">签名测试</router-link> -->
  </div>
</template>

<script lang="ts">
import {Vue, Component, Prop/* , Watch, Emit, Inject, Model, Provide */} from 'vue-property-decorator'
import App from '../App.vue'
import { AboutInfoModel, OpenSourceInfoModel } from '../models/AboutInfo'
import CMSApi from '../api/CMSApi'

import { Toast } from 'vant'
import { Icon } from 'vant'

Vue.use(Icon)
Vue.use(Toast)

@Component({})
export default class About extends App {
  pageTitle: string = '关于'
  myParent: App = (<App>this.$parent)
  about: AboutInfoModel = AboutInfoModel.MakeDef()

  constructor () {
    super()
    this.myParent.appPageTitle = this.pageTitle
    this.myParent.enableLeftButton()
  }

  created () {
    CMSApi.About().then((res) => {
      if (res.IsSucc) {
        this.about = <AboutInfoModel>res.Data
      } else {
        Toast.fail(res.Message)
      }
    })
    .catch((e) => {

    })
  }
}
</script>

<style scoped>
.about_sty{padding:0ex 2ex 0ex 2ex;}
.about_sty img{padding-left:auto;padding-right:auto;margin-left:auto;margin-right:auto;width:50%;}
.about_img{margin-left:auto;margin-right:auto;}
</style>
