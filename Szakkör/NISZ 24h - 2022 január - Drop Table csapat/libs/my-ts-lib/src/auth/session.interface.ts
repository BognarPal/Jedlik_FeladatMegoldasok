import { UserInterface } from "./user.interface";

export interface SessionInterface {
    id: number;
    user: UserInterface;
    lastAccessed: Date;
    token: string;
}