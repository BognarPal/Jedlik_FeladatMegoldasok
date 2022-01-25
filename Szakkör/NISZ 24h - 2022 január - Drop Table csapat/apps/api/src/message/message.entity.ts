import { MessageInterface } from '@catch-me/my-ts-lib';
import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';

@Entity()
export class MessageEntity implements MessageInterface {
  //Columns
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  author: string;

  @Column()
  message: string;

  @Column({default:0})
  likes: number;
}
