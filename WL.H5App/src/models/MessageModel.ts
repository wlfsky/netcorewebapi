
export class MessageModel {
  MsgId: string
  MsgText: string
  constructor (mid: string, msg: string) { this.MsgId = mid; this.MsgText = msg }
}
