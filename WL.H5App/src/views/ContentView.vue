<template>
    <div class="content_all">
      <van-row>
        <van-col span="24" class="title_sty">{{currContent.Title}}</van-col>
      </van-row>
      <van-row>
        <van-col span="16" class="author_sty">{{authorInfo}}</van-col>
        <van-col span="8">{{'*'}}</van-col>
      </van-row>
      <van-row>
        <van-col span="24" class="summary">{{currContent.Summary}}</van-col>
      </van-row>
      <van-row>
        <van-col span="24" class="content_body" v-html="currContent.Content">
        </van-col>
      </van-row>
      <van-row slot="footer">
        <van-col span="24">
          <AdView></AdView>
          <!-- <router-view name="content_adview"></router-view> -->
        </van-col>
      </van-row>
    </div>
</template>

<script lang="ts">
import {Vue, Component, Prop/* , Watch, Emit, Inject, Model, Provide */} from 'vue-property-decorator'
import App from '../App.vue'
import CMSApi, { ContentRequest } from '../api/CMSApi'
import { MessageModel } from '../models/MessageModel'
import {ContentModel} from '../models/ContentModel'
import Lib from '../common/Lib'

import { Button } from 'vant'
import { Panel } from 'vant'
import { Row, Col } from 'vant'
import { Toast } from 'vant'
import ImgView from '@/components/ImgView.vue'
import AdView from '@/components/AdView.vue'

Vue.use(Toast)
Vue.use(Row)
Vue.use(Col)
Vue.use(Panel)
Vue.use(Button)

@Component({
  components: {
    ImgView, AdView
  }
})
export default class ContentView extends App {
  pageTitle: string = '内容正文'
  myParent: App = (<App>this.$parent)

//   @Prop({type: ContentRequest, default: new ContentRequest('5', '5')}) contentRequest: ContentRequest
  contentRequest: ContentRequest
  currContent: ContentModel = new ContentModel()
  msg: MessageModel = new MessageModel('2', '默认消息')

  constructor () {
    super()
    this.myParent.appPageTitle = this.pageTitle
    this.myParent.enableLeftButton()
  }

  get authorInfo () {
    if(Lib.UndefOrNull(this.currContent) || Lib.UndefOrNull(this.currContent.Author) || Lib.UndefOrNull(this.currContent.LastEditTime)){
      return `Time -- Author`
    }
    return `${this.currContent.LastEditTime} -- ${this.currContent.Author}`
  }

  created () {
    //
    let contentId: string = this.$route.params.ContentId
    if(contentId !== undefined || contentId === null || contentId.trim() === ''){
      Toast.fail('参数不正确');
    }
    this.contentRequest = new ContentRequest('1', contentId)
    console.log(this.contentRequest)
    CMSApi.Content(this.contentRequest).then((res) => {
      console.log(res)
      if (res.IsSucc) {
        console.log('Content buss Succ')
        console.log(res)
        this.currContent = <ContentModel>res.Data
      } else {
        console.log('Content buss Fail');
        console.log(res)
      }
    }).catch((err) => {
      console.log('Content Err')
      console.log(err)
    })
    // --
    CMSApi.Message({ mid: '1' }).then((res) => {
      if (res.IsSucc) {
        console.log('Message buss Succ')
        console.log(res)
        this.msg = <MessageModel>res.Data
      } else {
        console.log('Message buss Fail')
        console.log(res)
      }
    }).catch((err) => {
      console.log('Message Fail')
      console.log(err)
    })
    //
  }
}
</script>

<style scoped>
.summary{color: teal;border:0.1ex solid teal;font-size: 0.3rem;padding:0.3rem 0.3rem 0.3rem 0.3rem;}
.mx{padding:0.5rem 0.5rem 0.5rem 0.5rem;margin:0.5rem 0.5rem 0.5rem 0.5rem}
.content_all{padding:0ex 2ex 0ex 2ex;}
.van-row{padding:1ex 0ex 0.5ex 0ex;}
.title_sty{font-size:2ex;font-weight:bold;}
.author_sty{font-size:0.8ex;}
.content_body{width:100%;}
.content_body p img{width:100%;padding-left:auto;padding-right:auto;}
</style>
