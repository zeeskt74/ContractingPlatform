export class AddContract {
  constructor(
    public mainContractorId: number,
    public relationContractorId: number
  ) {
  }
}

export class Contract {

  constructor(
    public id: number,
    public mainContractorId: number,
    public mainContractorName: string,
    public relationContractorId: number,
    public relationalContractorName: string
  ) {
  }
}
