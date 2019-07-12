import Action from './Action';

export class Result<T> {
    public Success: boolean = false;
    public Status: boolean = false;
    public Message: string = '';
    public Msg: string = '';
    public Data?: T;
    public ResponseAction?: Action;
    public Infos: string[] = new Array<string>();
    public Version: string = '';
    public get IsSucc(): boolean { return this.Success === true; }

    constructor(success: boolean, msg: string = '', data?: T) {
        this.Success = success;
        this.Status = success;
        this.Message = msg;
        this.Msg = msg;
        this.Data = data;
    }
}
