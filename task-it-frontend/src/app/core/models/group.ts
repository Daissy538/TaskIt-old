import { Member } from './member';

export class Group {
    id: number;
    name: string;
    description: string;
    members: Member[];

    public constructor(id: number, name: string, description: string) {
        this.id = id;
        this.name = name;
        this.description = description;
    }
}
