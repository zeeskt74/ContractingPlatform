export class Contractor {
  constructor(
    public id: number,
    public name: string) {
  }
}

export class ContractorDetail extends Contractor {
  constructor(
    public id: number,
    public name: string,
    public phone: string) {
      super(id, name);
  }
}
