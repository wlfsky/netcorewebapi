
export class AboutInfoModel {
  WebSite: string
  WebSiteVersion: string
  Remark: string
  ImgSrc: string
  LegalInfo: string
  OpenSourceInfo: Array<OpenSourceInfoModel>
  constructor (website: string, wsversion: string, remark: string, imgsrc: string = '', legalinfo: string = '') {
    this.WebSite = website
    this.WebSiteVersion = wsversion
    this.Remark = remark
    this.ImgSrc = imgsrc
    this.LegalInfo = legalinfo
    this.OpenSourceInfo = new Array<OpenSourceInfoModel>()
  }
  static MakeDef (): AboutInfoModel {
    let def: AboutInfoModel
    def = new AboutInfoModel('Vue+Typescript简易内容', '0.1.0', '备注：小试牛刀 raddleoj@aliyun.com', '/img.jpg', '法律信息')
    def.OpenSourceInfo.push(new OpenSourceInfoModel('Vue', '2.X'))
    def.OpenSourceInfo.push(new OpenSourceInfoModel('TypeScript', '3.X'))
    return def
  }
}

export class OpenSourceInfoModel {
  OpenSourceName: string
  Version: string
  constructor (osname: string = '', ver: string = '') {
    this.OpenSourceName = osname
    this.Version = ver
  }
}
