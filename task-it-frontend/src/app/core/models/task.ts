export class Task {
  ID: number;
  Title: string;
  Description: string;
  Status: string;
  Until: Date;
  From: Date;
  GroupID: number;
  GroupColor: string;
  TaskHolders: TaskHolder[];

  constructor(
    ID: number,
    title: string,
    description: string,
    status: string,
    until: Date,
    from: Date,
    groupID: number,
    groupColor: string,
    taskHolders: TaskHolder[]
  ) {
    this.ID = ID;
    this.Title = title;
    this.Description = description;
    this.Status = status;
    this.Until = until;
    this.From = from;
    this.GroupID = groupID;
    this.GroupColor = groupColor;
    this.TaskHolders = taskHolders;
  }
}

export class TaskOutgoing {
    Title: string;
    Description: string;
    Until: Date;
    From: Date;
    GroupID: number;
}

export class TaskHolder {
  ID: number;
  UserID: number;
  Username: string;
  Email: string;
}
