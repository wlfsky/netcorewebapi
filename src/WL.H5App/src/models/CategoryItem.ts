
export default class CategoryItem {
  CateId: string;
  ParentId: string
  CidPath: string
  Alias: string
  AliasPath: string
  Title: string
  Remark: string
  NewCount: number
  TotalCount: number

  constructor (cateid: string, title: string) { this.CateId = cateid; this.Title = title }
}
