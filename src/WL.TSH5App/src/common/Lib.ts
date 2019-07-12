/*eslint no-console: "error"*/

export default class Lib {
    // undefined和null空判断
    public static UndefinedOrNull(self: any): boolean {
        if (self === undefined || self === null) {
            return true;
        }
        return false;
    }

    public static UndefOrNull(self: any): boolean {
        // undefined和null空判断 简化版
        return this.UndefinedOrNull(self);
    }

    public static AnyUndefOrNull(self: any[]): boolean {
        // 队列中任何一个undefined和null空判断
        for (const item of self) {
            if (this.UndefOrNull(item)) {
                return true;
            }
        }
        return false;
    }

    public static G(self: () => any, execute: (obj: any) => void): void {
        // 迭代器，外部提供迭代方法，和处理迭代方法
        const res = self();
        while (true) {
            const currRes = res.next();
            if (currRes.done === true) {
                break;
            }// done了就退出循环
            if (execute !== null) {
                execute(currRes.value);
            }
        }
    }

    public static log(msg: string | any, obj?: object | any | undefined | null, ...other: any[]): void {
        // custom console
        const logFunc = console.log;
        logFunc(msg);
        if (typeof obj !== 'undefined' && obj !== null) {
            logFunc(obj);
        }
        if (typeof other === 'object' && typeof other.length !== 'undefined' && other.length > 0) {
            for (const item of other) {
                logFunc(item);
            }
        }
    }
}

