import {Entity, Column, PrimaryGeneratedColumn} from "typeorm";
import { RoleInterface } from '@catch-me/my-ts-lib';

@Entity({name : 'roles'})
export class RoleEntity implements RoleInterface {

    @PrimaryGeneratedColumn()
    id: number;

    @Column({
        length: 30,
        unique: true
    })
    name: string;
}