
export default class CategoryItem {
  Cid: string
  ParentId: string
  CidPath: string
  Alias: string
  AliasPath: string
  Title: string
  Remark: string
  HaveNew: number

  constructor (cid: string, title: string) { this.Cid = cid; this.Title = title }
}
