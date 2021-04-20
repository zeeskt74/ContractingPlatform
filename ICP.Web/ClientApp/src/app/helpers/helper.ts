export default class Helper {
  static isNullOrUndefined<T>(object: T | undefined | null): object is T {
    return <T>object !== undefined && <T>object !== null;
  }
}
