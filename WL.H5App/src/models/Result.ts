
export class Result<T> {
  Status: boolean
  Message: string
  Msg: string
  Data: T
  ResponseAction: Action
  Infos: Array<string>
  Version: string
  get IsSucc (): boolean { return this.Status === true }
  constructor (status: boolean, msg: string = '', data?: T) {
    this.Status = status
    this.Message = msg
    this.Msg = msg
    this.Data = data
  }
}

export class Action {
  Condition: string
  ActionType: string
  Action: string
}
