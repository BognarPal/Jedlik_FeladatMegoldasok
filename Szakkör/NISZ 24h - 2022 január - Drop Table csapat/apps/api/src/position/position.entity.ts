import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';



@Entity()
export class PositionEntity{
  //Columns
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  playground_id: number;

  @Column()
  user_id: number;

  @Column()
  turn: number;
  
  @Column()
  used_vehicle: number;

}
