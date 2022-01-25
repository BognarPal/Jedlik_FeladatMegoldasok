import { HttpStatus, Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { LobbyEntity } from './lobby.entity';
import { AuthService } from '../auth/auth.service';
import { SessionEntity } from '../auth/session.entity';
import { OperationException } from 'libs/my-ts-lib/src/errorhandling';
import { UserEntity } from '../auth/user.entity';

interface PlayerNamesInterface {
  lobby_id: string;
  admin_name: string;
  user_names: string;
}

@Injectable()
export class LobbyService {
  constructor(
    @InjectRepository(LobbyEntity)
    private readonly lobbyRepository: Repository<LobbyEntity>,
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>,
    private authService: AuthService
  ) {}

  async all(): Promise<PlayerNamesInterface[]> {
    const lobbys: LobbyEntity[] = await this.lobbyRepository.find();
    const backk: PlayerNamesInterface[] = await Promise.all(
      lobbys.map((lobby) => {
        return this.DataByLobby(lobby).then((res) => {
          return res;
        });
      })
    );

    // Promise.all(lobbys.map(lobby => {
    //   return async () => {

    // const myLobby = ;
    // const userNames = myLobby.user_ids.split(",").filter(w => w != "").map(r => Number(r));

    // let newUserNames = "";

    // const adminName = (await this.GetUserNameById(myLobby.admin_id));

    // const users = await Promise.all(userNames.map(i => {
    //   return this.GetUserNameById(i);
    // }));

    // users.forEach(user => {
    //   newUserNames += ","+ user;
    // })
    //  {lobby_id:myLobby.id.toString(),admin_name:adminName,user_names:newUserNames};

    //   }
    // }));

    // userNames.forEach(async i => {
    //    newUserNames += (await this.GetUserNameById(i));
    // }
    // )

    // await Promise.all(
    //   userNames.map(i => {
    //     return async () => {
    //       console.log("asdsaddsadsa");
    //       newUserNames += await this.GetUserNameById(i);
    //     };
    //   })
    // );

    // Promise.all(userNames.map(i => {
    //   return async () => {
    //     newUserNames += await this.GetUserNameById(i);
    //     console.log("anyád");
    //   };
    // }));

    // console.log(lobbys);
    // lobbys.forEach(async a =>
    //   {
    //     const backPlayers:PlayerNamesInterface = ;

    //     backPlayers.admin_name = (await this.GetUserNameById(a.admin_id));
    //     backPlayers.user_names = a.user_ids.split(",").filter(w => w != "").map(r => Number(r)).toString();
    //     console.log(backPlayers);
    //     back.push(backPlayers);
    //   }
    // );

    // console.log(back);
    return backk;
  }
  async join(id: number, token) {
    const session: SessionEntity = await this.authService.getSession(token);
    if (await this.IsTherePlaceLeft(id)) {
      if (await this.LobbyPlayersContains(id, session.user.id)) {
        throw new OperationException('INVALID_BODY', HttpStatus.BAD_REQUEST);
      } else {
        this.AddToPlayers(id, session.user.id);
        return 'done';
      }
    } else {
      return 'This lobby is full';
    }
  }

  async quit(id: number, token) {
    const session: SessionEntity = await this.authService.getSession(token);
    this.RemoveFromPlayers(id, session.user.id);
    return 'done';
  }

  
  async quitAll(token) {
    const session: SessionEntity = await this.authService.getSession(token);
    //this.RemoveFromPlayers(id, session.user.id);

    await Promise.all(
      await (await this.lobbyRepository.find()).map(r => r.id).map((lobby_id) => {
        this.RemoveFromPlayers(lobby_id, session.user.id);
      })
    );

    return 'done';
  }
  
  async find(id: number) {
    return this.DataByLobby(await this.lobbyRepository.findOne(id)).then((res) => {return res;})
  }

  async create(token): Promise<PlayerNamesInterface> {
    const session: SessionEntity = await this.authService.getSession(token);
    if (!session.id) {
      throw new OperationException('INVALID_BODY', HttpStatus.BAD_REQUEST);
    }
    if (
      await this.lobbyRepository.findOne({
        where: { admin_id: session.user.id },
      })
    ) {
      throw new OperationException('INVALID_BODY', HttpStatus.BAD_REQUEST);
    }
    return this.find((await this.lobbyRepository.save({ admin_id: session.user.id })).id);
  }

  // async get(id: number): Promise<LobbyEntity> {
  //   return this.lobbyRepository.findOne(id);
  // }

  // async update(id: number, data: { admin_id: number }): Promise<any> {
  //   return this.lobbyRepository.update(id, data);
  // }

  async delete(id: number, token): Promise<any> {
    const session: SessionEntity = await this.authService.getSession(token);
    const lobby: LobbyEntity = await this.lobbyRepository.findOne({
      where: { admin_id: session.user.id },
    });
    if (lobby.admin_id == session.user.id) {
      return this.lobbyRepository.delete(id);
    } else {
      throw new OperationException(
        'INVALID_LOBBY_ROLE',
        HttpStatus.BAD_REQUEST
      );
    }
  }

  private async GetUserNameById(id: number): Promise<string> {
    return (await this.userRepository.findOne(id)).name;
  }
  private async LobbyPlayersContains(
    LobbyId: number,
    PlayerId: number
  ): Promise<boolean> {
    const myLobby = await this.lobbyRepository.findOne(LobbyId);
    const neww = myLobby.user_ids.split(',');
    neww.shift();
    if (neww.includes(PlayerId.toString())) {
      return true;
    } else {
      return false;
    }
  }

  private async DataByLobby(g: LobbyEntity): Promise<PlayerNamesInterface> {
    const myLobby = g;
    const userNames = myLobby.user_ids
      .split(',')
      .filter((w) => w != '')
      .map((r) => Number(r));

    let newUserNames = '';

    const adminName = await this.GetUserNameById(myLobby.admin_id);

    const users = await Promise.all(
      userNames.map((i) => {
        return this.GetUserNameById(i);
      })
    );

    users.forEach((user) => {
      newUserNames += ',' + user;
    });
    return {
      lobby_id: myLobby.id.toString(),
      admin_name: adminName,
      user_names: newUserNames,
    };
  }

  private async AddToPlayers(id: number, player_id: number) {
    const myLobby = await this.lobbyRepository.findOne(id);
    myLobby.user_ids += ',' + player_id.toString();
    this.lobbyRepository.update(id, myLobby);
  }

  private async IsTherePlaceLeft(id: number): Promise<boolean> {
    const myLobby = await this.lobbyRepository.findOne(id);
    myLobby.user_ids.split(',').shift();
    const lng = myLobby.user_ids.length / 2;
    if (lng < 5) {
      return true;
    } else {
      return false;
    }
  }

  private async RemoveFromPlayers(id: number, player_id: number) {
    const myLobby = await this.lobbyRepository.findOne(id);
    const myLobbysArray = myLobby.user_ids
      .split(',')
      .filter((w) => w != '')
      .map((r) => Number(r));
    if (myLobbysArray.includes(player_id)) {
      for (let i = 0; i < myLobbysArray.length; i++) {
        if (myLobbysArray[i] === player_id) {
          myLobbysArray.splice(i, 1);
        }
      }
    }
    myLobby.user_ids = '';
    myLobbysArray.forEach((i) => {
      myLobby.user_ids += ',' + i;
    });
    this.lobbyRepository.update(id, myLobby);
  }
}
