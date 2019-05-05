export class NfcScan {
    constructor(
      public scanId: number,
      public uid: number,
      public dateTime: Date,
      public deviceId: number,
    ) {}
  }
