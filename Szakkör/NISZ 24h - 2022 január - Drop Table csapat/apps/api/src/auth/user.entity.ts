import {Entity, Column, PrimaryGeneratedColumn, ManyToMany, JoinTable} from "typeorm";
import { UserInterface } from "@catch-me/my-ts-lib";
import { RoleEntity } from "./role.entity";

@Entity({name: 'users'})
export class UserEntity implements UserInterface {

    @PrimaryGeneratedColumn()
    id: number;

    @Column({
        length: 200,
        unique: true
    })
    email: string;

    @Column({
        length: 100
    })
    name: string;

    @Column({
        length: 200
    })
    passwordHash: string;

    @ManyToMany(() => RoleEntity)
    @JoinTable({name: 'users_roles'})
    roles: RoleEntity[];
}