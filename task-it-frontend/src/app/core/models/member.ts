export class Member {
  userId: number;
  userName: string;
  groupId: number;
  groupName: string;
  dateOfSubscription: Date;

  public constructor(
    userId: number,
    userName: string,
    groupId: number,
    groupName: string,
    dateOfSubscription: Date
  ) {
    this.userId = userId;
    this.userName = userName;
    this.groupId = groupId;
    this.groupName = groupName;
    this.dateOfSubscription = dateOfSubscription;
  }
}
