
<template>
<div>
    <van-row type="flex" justify="center">
      <van-col span="18" class="main-title-sty">学不死就往死里学！</van-col>
    </van-row>
    <div v-for="cate in categoryList" :key="cate.CateId">
    <van-cell v-bind:value="cate.Remark" icon="shop" is-link :to="{path: `/Category/${cate.Cid}`}">
    <template slot="title">
        <span>{{cate.Title}}</span>
        <van-tag type="danger">{{cate.NewCount}}</van-tag>
    </template>
    </van-cell>
    </div>
</div>
</template>

<script lang="ts">
import {Vue, Component, Prop/* , Watch, Emit, Inject, Model, Provide */} from 'vue-property-decorator'
import App from '../App.vue'
import CMSApi, { CategoryRequest } from '../api/CMSApi'
import CategoryItem from '../models/CategoryItem'

import { Row, Col } from 'vant'
import { Cell, CellGroup } from 'vant'
import { Tag } from 'vant'
import { Toast } from 'vant'

Vue.use(Toast)
Vue.use(Tag)
Vue.use(Cell)
Vue.use(CellGroup)
Vue.use(Col)
Vue.use(Row)

@Component({components: {}})
export default class IndexView extends App {
  pageTitle: string = '主题列表'
  categoryList: Array<CategoryItem> = new Array<CategoryItem>()
  myParent: App = (<App>this.$parent)

  constructor () {
    super()
    this.myParent.appPageTitle = this.pageTitle
    this.myParent.disableLeftButton()
  }

  created () {
    CMSApi.RootCategory(new CategoryRequest('1')).then((res) => {
      console.log('== RootCategory Result');
      console.log(res);
      if (res.IsSucc) {
        console.log('RootCategory buss Succ');
        console.log(res);
        this.categoryList = <Array<CategoryItem>>res.Data;
        console.log(this.categoryList)
      } else {
        console.log('RootCategory buss Fail');
        console.log(res);
      }
    }).catch((err) => {
      console.log('RootCategory Err');
      console.log(err);
    })
  }
}
</script>

<style scoped>
.main-title-sty{text-align:center;font-weight:bold;font-size:2ex;border-width:0.1px;border:0.1ex solid darkred;}
</style>
