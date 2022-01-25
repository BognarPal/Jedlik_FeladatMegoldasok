import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';
import { LobbyInterface } from '../../../../libs/my-ts-lib/src/lobby/Lobby.interface';



@Entity()
export class LobbyEntity implements LobbyInterface {
  //Columns
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  admin_id: number;

  @Column({default:""})
  user_ids: string;

}
