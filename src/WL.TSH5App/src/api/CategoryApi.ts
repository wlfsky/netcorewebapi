import HttpClient from '../common/HttpClient';
import { Result } from '../models/Result';
import CategoryItem from '../models/CategoryItem';
import ThenPromise from 'promise';
import CategoryRequest from '../models/CategoryRequest';

export default class CategoryApi {
    public static CallApi(param: CategoryRequest): ThenPromise<Result<CategoryItem[]>> {
        return HttpClient.Get(`api/category/${param.id}`, param);
    }

    public static Succ<T>(msg: string, data: T): Result<T> {
        const r = new Result<T>(true, msg, data);
        return r;
    }

    public static Fail<T>(msg: string, data: T): Result<T> {
        const r = new Result<T>(false, msg, data);
        return r;
    }
}

