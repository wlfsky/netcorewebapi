
export class ContentModel {
  ContentId: string
  RootCategory: CategoryModel
  ParentCategory: Array<CategoryModel>
  ContentAlias: string
  AliasPath: string
  Keys: Array<string>
  Tags: string[]
  Title: string
  Summary: string
  Content: string
  Remark: string
  Author: string
  CreateTime: Date
  LastEditTime: Date
  MainPic: string
  Files: Array<FileModel>
  Pics: Array<PicModel>
  Videos: Array<VideoModel>
  Logs: Array<LogModel>
  Previous: NeighbourModel
  Next: NeighbourModel
  SysInfo: SysInfoModel
}

export class CategoryModel {
  CategoryId: string
  CategoryTitle: string
  CategoryAlias: string
  Level: number
}

export class FileModel {
  Id: string
  Key: string
  Name: string
  Extension: string
  ByteSize: number
  Path: string
}

export class PicModel {
  Id: string
  Key: string
  Name: string
  Extension: string
  ByteSize: number
  PicSize: ImageSize
  Path: string
}

export class VideoModel {
  Id: string
  Key: string
  Name: string
  Extension: string
  ByteSize: number
  Duration: number
  VideoInfo: string
  Path: string
}

export class ImageSize {
  width: number
  height: number
}

export class LogModel {
  Id: string
  Operator: string
  Remark: string
  OperationTime: Date
}

export class SysInfoModel {
  IsDel: boolean
  IsShow: boolean
  SysStatus: number
  SysStatusName: string
  SysRemark: string
  SysRemarkShow: string
}

export class NeighbourModel {
  Id: string
  Title: string
  ShowTitle: string
}

export class ContentListItemModel {
  ContentId: string
  ParentCategory: Array<CategoryModel>
  ContentAlias: string
  AliasPathte: string
  Keys: Array<string>
  Title: string
  Summary: string
  CreateTime: Date
  LastEditTime: Date
  MainPic: string
}

export class Page<T> {
  PageSize: number
  PageIndex: number
  Total: number
  TotalPage: number
  Rows: Array<T>
}
