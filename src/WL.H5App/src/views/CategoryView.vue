<template>
    <div>
    <div v-for="cate in categoryList" :key="cate.CateId">
    <van-cell v-bind:value="cate.Remark" icon="shop" is-link :to="{path: `/ContentListView/${cate.CateId}`, params: { categoryid: cate.CateId }}">
    <template slot="title">
        <span>{{cate.Title}}</span>
        <van-tag type="danger">{{cate.NewCount}}</van-tag>
    </template>
    </van-cell>
    </div>
</div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator'
// import { Watch, Emit, Inject, Model, Prop, Provide } from 'vue-property-decorator'
import App from '../App.vue'
import CMSApi from '../api/CMSApi'
import CategoryItem from '../models/CategoryItem'

import { Cell, CellGroup } from 'vant'
import { Tag } from 'vant'
import { Toast } from 'vant'

Vue.use(Toast)
Vue.use(Tag)
Vue.use(Cell)
Vue.use(CellGroup)

@Component({})
export default class Category extends App {
  pageTitle: string = '子分类列表'
  myParent: App = (<App>this.$parent)
  categoryList: Array<CategoryItem> = []

  constructor () {
    super()
    this.myParent.appPageTitle = this.pageTitle
    this.myParent.enableLeftButton()
  }

  created () {
    console.log('category route query')
    console.log(this.$route)
    if(typeof(this.$route.params.cid)==='undefined'||this.$route.params.cid===null){
      Toast.fail('参数不正确');
    }
    let currCategoryId: string = this.$route.params.cid;
    let resx = CMSApi.Category({ id: currCategoryId }).then((res) => {
      this.categoryList = res.Data
    }).catch((err) => {
      console.log(err)
      console.log(err.Message)
    })
    console.log(resx)
  }
}
</script>

<style scoped lang="less">

</style>
