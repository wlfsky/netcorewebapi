<template>
    <div>
<van-row>
  <van-col span="24">{{currContent.Title}}</van-col>
</van-row>
<van-row>
  <van-col span="16">{{`${currContent.Author} -- ${currContent.LastEditTime}`}}</van-col>
  <van-col span="8">{{'*'}}</van-col>
</van-row>
<van-row>
  <van-col span="24" class="summary">{{currContent.Summary}}</van-col>
</van-row>
<van-row>
  <van-col span="24">{{currContent.Content}}</van-col>
</van-row>
<van-row slot="footer">
  <van-col span="24">底部</van-col>
</van-row>
    </div>
</template>

<script lang="ts">
import {Vue, Component, Prop/* , Watch, Emit, Inject, Model, Provide */} from 'vue-property-decorator'
import CMSApi, { ContentRequest } from '../api/CMSApi'
import { MessageModel } from '../models/MessageModel'
import {ContentModel} from '../models/ContentModel'
import { Button } from 'vant'
import { Panel } from 'vant'
import { Row, Col } from 'vant'
import { Toast } from 'vant'

Vue.use(Toast)
Vue.use(Row)
Vue.use(Col)
Vue.use(Panel)
Vue.use(Button)

@Component({})
export default class ContentView extends Vue {
  name: string = ''
  title: string = ''
//   @Prop({type: ContentRequest, default: new ContentRequest('5', '5')}) contentRequest: ContentRequest
  contentRequest: ContentRequest
  currContent: ContentModel = new ContentModel()
  msg: MessageModel = new MessageModel('2', '默认消息')

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

<style lang="less" scoped>
.summary{color: teal;border:0.1ex solid teal;font-size: 0.3rem;padding:0.3rem 0.3rem 0.3rem 0.3rem;}
.mx{padding:0.5rem 0.5rem 0.5rem 0.5rem;margin:0.5rem 0.5rem 0.5rem 0.5rem}
</style>
