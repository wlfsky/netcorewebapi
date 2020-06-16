<template>
    <div>
        <van-list v-model="loading" :finished="finished" finished-text="没有更多了" @load="onLoad">
            <template v-for="item in page_list.Rows">
            <van-cell v-bind:key="item.ContentId" @click="toContent(item.ContentId)" >
                <van-row class="title_sty">
                    <van-col span="24">{{item.Title}}</van-col>
                </van-row>
                <van-row class="author-sty">
                    <van-col span="16">{{`${item.LastEditTime} -- ${item.Author}`}}</van-col>
                    <van-col span="8" class="author-start-sty">{{'*'}}</van-col>
                </van-row>
                <van-row>
                    <van-col v-if="item.MainPic" span="24"><img v-bind:src="item.MainPic"/></van-col>
                </van-row>
                <van-row>
                    <van-col span="24">{{item.Summary}}</van-col>
                </van-row>
            </van-cell>
            </template>
        </van-list>
    </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator'
import App from '../App.vue'
import CMSApi, { PageRequest, ContentPageCondition } from '../api/CMSApi'
import { Page, ContentListItemModel } from '../models/ContentModel'
import { List } from 'vant'
import { Cell, CellGroup } from 'vant'
import { Row, Col } from 'vant'
import { Toast } from 'vant'

Vue.use(Toast)
Vue.use(List)
Vue.use(Cell)
Vue.use(Row)
Vue.use(Col)

@Component({})
export default class ContentListView extends App {
    pageTitle: string = '内容列表'
    myParent: App = <App>this.$parent
    list: number[] = []
    loading: boolean = false
    finished: boolean = false
    page_list: Page<ContentListItemModel> = { PageSize: 3, PageIndex: 1, Rows: [] } as Page<ContentListItemModel>;
    currPageSize: number = 2
    condition: PageRequest<ContentPageCondition> = {PageSize: this.currPageSize, PageIndex: 1, Condition: {PCId:'0'}} as PageRequest<ContentPageCondition>

    constructor () {
      super()
      this.myParent.appPageTitle = this.pageTitle
      this.myParent.enableLeftButton()
    }

    onLoad() {
        console.log('onload...begin')
        this.condition.PageIndex++
        this.loadContent(this.condition)
        console.log('onload...end')
    }

    toContent (cid: string) {
        this.$router.push({path: `/ContentView/${cid}`, params: {ContentId: cid}})
    }

    loadContent (condition: PageRequest<ContentPageCondition>) {
        if(condition.PageSize<1){
            return
        }
        if(condition.PageIndex<1){
            return
        }
        if(condition.Condition.PCId===null||condition.Condition.PCId===''){
            return
        }
        this.loading = true;
        CMSApi.ContentPage(condition).then((res) => {
            if (res.IsSucc) {
                let currPageRows = res.Data as Page<ContentListItemModel>
                if(currPageRows !== null && currPageRows.Rows !== null && currPageRows.Rows.length>0){
                    for (let row of currPageRows.Rows)
                    this.page_list.Rows.push(row)
                }
                this.loading = false;
                this.finished = true;
            } else {
                // 
                this.loading = false;
                this.finished = true;
            }
        }).catch((err) => {
            // 
            this.loading = false;
            this.finished = true;
        })
    }

    created () {
        let categoryid: string = this.$route.params.CategoryId
        if (categoryid === null || categoryid === '') {
            categoryid = this.$route.query['CategoryId']
        }
        //
        this.condition.Condition.PCId = categoryid
        //this.loadContent(this.condition)
    }
}
</script>

<style scoped>
.title_sty{font-weight:bold;}
.author-sty{font-size:0.2ex;}
.author-start-sty{text-align:right;}
</style>
