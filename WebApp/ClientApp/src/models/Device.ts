export class Device {
    constructor(
      public deviceId: number,
      public deviceIdReal: number,
      public pendingDeviceId: number,
      public updatePending: boolean,
      public classroomId: number,
    ) {}
  }
