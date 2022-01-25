import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';



@Entity()
export class PlaygroundEntity{
  //Columns
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  criminal_id: number;

  @Column({default:""})
  user_ids: string;
  
  @Column()
  in_progress: boolean;

  @Column()
  turn: number;
}
