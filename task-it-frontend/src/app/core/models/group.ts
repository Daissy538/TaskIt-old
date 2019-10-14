import { Member } from './member';

export class GroupOutgoing {
  id: number;
  name: string;
  description: string;
  iconID: number;
  colorID: number;

  public constructor(
    id: number,
    name: string,
    description: string,
    iconID: number,
    colorID: number
  ) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.iconID = iconID;
    this.colorID = colorID;
  }
}

export class GroupIncoming {
  id: number;
  name: string;
  description: string;
  iconValue: string;
  iconName: string;
  colorValue: string;
  colorName: string;
  members: Member[];

  public constructor(
    id: number,
    name: string,
    description: string,
    iconValue: string,
    iconName: string,
    colorValue: string,
    colorName: string,
    members: Member[]
  ) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.iconValue = iconValue;
    this.iconName = iconName;
    this.colorName = colorName;
    this.colorValue = colorValue;
    this.members = members;
  }
}
