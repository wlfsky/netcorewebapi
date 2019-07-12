
export default class Lib {
  // undefined和null空判断
  static UndefinedOrNull (self: any): boolean {
    if (self === undefined || self === null) {
      return true
    }
    return false
  }

  static UndefOrNull (self: any): boolean {
    return this.UndefinedOrNull(self)
  }

  static AnyUndefOrNull (self: Array<any>): boolean {
    for (const item of self) {
      if (this.UndefOrNull(item)) {
        return true
      }
    }
    return false
  }
}
