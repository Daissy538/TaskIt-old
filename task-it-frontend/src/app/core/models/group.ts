import { Member } from './member';

export class Group {
    id: number;
    name: string;
    description: string;
    icon: string;
    color: string;
    members: Member[];

    public constructor(id: number, name: string, description: string, icon: string, color: string) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.color = color;
    }
}
