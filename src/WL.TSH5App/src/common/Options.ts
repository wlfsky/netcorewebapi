import KeyValue from './KeyValue';

export default class Options {
    public method: string = '';
    public headers: KeyValue[] = new Array<KeyValue>();
    public cookies: KeyValue[] = new Array<KeyValue>();
}
