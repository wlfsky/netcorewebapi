import HttpClient from '../common/HttpClient'
import { Result } from '../models/Result'
import CategoryItem from '../models/CategoryItem'
import ThenPromise from 'promise'
import { CategoryRequest } from '../api/CMSApi'

export default class CategoryApi {
  static CallApi (param: CategoryRequest): ThenPromise<Result<Array<CategoryItem>>> {
    let url = `api/category/${param.id}`;
    console.log(`分类获取分类列表地址：${url}`);
    return HttpClient.Get(url, param);
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

