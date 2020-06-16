import HttpClient from '../common/HttpClient'
import { Result } from '../models/Result'
import { ContentModel, ContentListItemModel, Page } from '../models/ContentModel'
import CategoryItem from '../models/CategoryItem'
import ThenPromise from 'promise'
import { MessageModel } from '../models/MessageModel'
import { AboutInfoModel, OpenSourceInfoModel } from '../models/AboutInfo'

export default class CMSApi {
  static GetApi<Tin, Tou> (url: string, param: Tin): ThenPromise<Result<Tou>> {
    return HttpClient.Get(url, param)
  }
  static PostApi<Tin, Tou> (url: string, param: Tin): ThenPromise<Result<Tou>> {
    return HttpClient.Post(url, param)
  }
  // 根分类获取
  static RootCategory (param: CategoryRequest): ThenPromise<Result<Array<CategoryItem>>> {
    let url = `api/rootcategory/${param.id}`;
    return HttpClient.Get<CategoryRequest, Result<Array<CategoryItem>>>(url, param)
  }
  // 第一分类
  static FirstCategory (param: CategoryRequest): ThenPromise<Result<Array<CategoryItem>>> {
    return HttpClient.Get<CategoryRequest, Result<Array<CategoryItem>>>(`api/firstcategory/${param.id}`, param)
  }
  // 第二分类
  static SecondCategory (param: CategoryRequest): ThenPromise<Result<Array<CategoryItem>>> {
    return HttpClient.Get<CategoryRequest, Result<Array<CategoryItem>>>(`api/secondcategory/${param.id}`, param)
  }
  // 老分类
  static Category (param: CategoryRequest): ThenPromise<Result<Array<CategoryItem>>> {
    return HttpClient.Get<CategoryRequest, Result<Array<CategoryItem>>>(`api/category/${param.id}`, param)
  }
  // 单个内容
  static Content (param: ContentRequest): ThenPromise<Result<ContentModel>> {
    return HttpClient.Get<ContentRequest, Result<ContentModel>>(`api/content/${param.ContentId}`, param)
  }
  // 内容列表
  static ContentPage (param: PageRequest<ContentPageCondition>): ThenPromise<Result<Page<ContentListItemModel>>> {
    return HttpClient.Post<PageRequest<ContentPageCondition>, Result<Page<ContentListItemModel>>>(`api/contentpage`, param)
  }
  // 消息
  static Message (param: MessageRequest): ThenPromise<Result<MessageModel>> {
    return HttpClient.Get<MessageRequest, Result<MessageModel>>(`api/msg/${param.mid}`, param)
    // return CMSApi.Fail<MessageResponse>('未知错误', null)
  }
  // 关于信息
  static About (): ThenPromise<Result<AboutInfoModel>> {
    return HttpClient.Get<null, Result<AboutInfoModel>>(`api/about`, null)
    // return CMSApi.Fail<MessageResponse>('未知错误', null)
  }

  static Succ<T> (msg: string, data: T): Result<T> {
    let r = new Result<T>(true, msg, data)
    return r
  }

  static Fail<T> (msg: string, data: T): Result<T> {
    let r = new Result<T>(false, msg, data)
    return r
  }
}

export class CategoryRequest {
  id: string
  constructor (categoryid: string) { this.id = categoryid }
}

export class ContentRequest {
  CategoryId: string
  ContentId: string
  constructor (categoryid: string, contentid: string) { this.CategoryId = categoryid; this.ContentId = contentid }
}

export class MessageRequest {
  mid: string
}

export class PageRequest<T> {
  PageSize: number
  PageIndex: number
  Condition: T
}

export class ContentPageCondition {
  PCId: string // ParentCategoryId
  Key: string
  TitleKey: string
}
