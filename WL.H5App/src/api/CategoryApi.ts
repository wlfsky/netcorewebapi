import HttpClient from '../common/HttpClient'
import { Result } from '../models/Result'
import CategoryItem from '../models/CategoryItem'
import ThenPromise from 'promise'

export default class CategoryApi {
  static CallApi (param: CategoryRequest): ThenPromise<Result<Array<CategoryItem>>> {
    return HttpClient.Get(`api/category/${param.id}`, param)
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
  id: number
}
