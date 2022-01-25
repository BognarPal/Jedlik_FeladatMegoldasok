import {Entity, Column, PrimaryGeneratedColumn, ManyToOne, JoinColumn} from "typeorm";
import { UserEntity } from "./user.entity";

@Entity({name: 'sessions'})
export class SessionEntity {

    @PrimaryGeneratedColumn()
    id: number;

    @ManyToOne(() => UserEntity)
    @JoinColumn({name : 'userId', referencedColumnName: 'id'})
    user: UserEntity;

    @Column()
    lastAccess: Date;

    @Column({
        length: 200,
        unique: true
    })
    token: string;

    public constructor(init?:Partial<SessionEntity>) {
        if (init)
            Object.assign(this, init);
    }
}