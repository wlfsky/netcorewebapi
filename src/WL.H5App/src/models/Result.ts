
export class Result<T> {
  //Status: boolean;
  Success: boolean;
  Message: string
  Msg: string
  Data: T
  ResponseAction: Action = new Action();
  Infos: Array<string> = new Array<string>();
  Version: string = "";
  get IsSucc (): boolean { return this.Success === true }

  constructor (success: boolean, msg: string = '', data?: T) {
    //this.Status = success;
    this.Success = success;
    this.Message = msg;
    this.Msg = msg;
    this.Data = data;
  }
}

export class Action {
  Condition: string = "";
  ActionType: string = "";
  Action: string = "";
}
