import { RoleInterface } from "./role.interface";

export interface UserInterface {
    id: number;
    email: string;
    name: string;
    roles: RoleInterface[]    
}