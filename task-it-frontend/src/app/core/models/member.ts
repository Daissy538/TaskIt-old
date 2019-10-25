export class Member {
  userID: number;
  userName: string;
  groupID: number;
  groupName: string;
  dateOfSubscription: Date;

  public constructor(
    userID: number,
    userName: string,
    groupID: number,
    groupName: string,
    dateOfSubscription: Date
  ) {
    this.userID = userID;
    this.userName = userName;
    this.groupID = groupID;
    this.groupName = groupName;
    this.dateOfSubscription = dateOfSubscription;
  }
}
