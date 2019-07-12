import HttpClient from '../common/HttpClient'
import { Result } from '../models/Result'
import { ContentModel } from '../models/ContentModel'
import ThenPromise from 'promise'

export default class ContentApi {
  public static CallApi (param: CategoryRequest): ThenPromise<Result<ContentModel>> {
    return HttpClient.Get(`api/content/${param.id}`, param)
  }

  public static Succ<T> (msg: string, data: T): Result<T> {
    let r = new Result<T>(true, msg, data)
    return r
  }

  public static Fail<T> (msg: string, data: T): Result<T> {
    let r = new Result<T>(false, msg, data)
    return r
  }
}

export class CategoryRequest {
  id: number
}
