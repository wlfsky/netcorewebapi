
export default class CategoryItem {
    public CateId: string = '';
    public ParentId: string = '';
    public CidPath: string = '';
    public Alias: string = '';
    public AliasPath: string = '';
    public Title: string = '';
    public Remark: string = '';
    public NewCount: number = 0;
    public TotalCount: number = 0;

    constructor(cateid: string, title: string) { this.CateId = cateid; this.Title = title; }
}

