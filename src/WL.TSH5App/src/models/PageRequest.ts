
export default class PageRequest<T> {
    public PageSize: number = 10;
    public PageIndex: number = 1;
    public Condition?: T;
}

