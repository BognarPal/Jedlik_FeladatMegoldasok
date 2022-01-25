import { HttpStatus, Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { PlaygroundEntity } from './playground.entity';
import { AuthService } from '../auth/auth.service';
import { UserEntity } from '../auth/user.entity';
import { PlaygroundInterface } from './playground.interface';
import { LobbyEntity } from '../lobby/lobby.entity';
import { SessionEntity } from '../auth/session.entity';
import { OperationException } from '@catch-me/my-ts-lib';


@Injectable()
export class PlaygroundService {
  constructor(
     @InjectRepository(PlaygroundEntity)
     private readonly playgroundRepository: Repository<PlaygroundEntity>,
     private readonly authService: AuthService,
     @InjectRepository(LobbyEntity)
     private readonly lobbyRepository: Repository<LobbyEntity>,
  ) {}

  async create(token): Promise<PlaygroundInterface> {
    const session: SessionEntity = await this.authService.getSession(token);
    if (!session.id) {
      throw new OperationException('INVALID_BODY', HttpStatus.BAD_REQUEST);
    }
    const lobby = await this.lobbyRepository.findOne({where: { admin_id: session.user.id },});
    if (lobby)
    {
      return this.playgroundRepository.save({criminal_id:lobby.admin_id,user_ids:lobby.user_ids,in_progress:true,turn:1});
    }
    else{
      throw new OperationException('INVALID_BODY', HttpStatus.BAD_REQUEST);
    }
  }

  async endGame(token): Promise<any> {
    const session: SessionEntity = await this.authService.getSession(token);
    if (!session.id) {
      throw new OperationException('INVALID_BODY', HttpStatus.BAD_REQUEST);
    }
    const game = this.playgroundRepository.findOne({where: { admin_id: session.user.id },});
    if (game)
    {
      return this.playgroundRepository.update((await game).id,{in_progress:false});
    }
    else{
      throw new OperationException('INVALID_BODY', HttpStatus.BAD_REQUEST);
    }
  }
  
  async next(token): Promise<any> {
    const session: SessionEntity = await this.authService.getSession(token);
    if (!session.id) {
      throw new OperationException('INVALID_BODY', HttpStatus.BAD_REQUEST);
    }
    const game = this.playgroundRepository.findOne({where: { criminal_id: session.user.id },});
    if (game)
    {
      return this.playgroundRepository.update((await game).id,{turn:(await game).turn+1});
    }
    else{
      throw new OperationException('INVALID_BODY', HttpStatus.BAD_REQUEST);
    }
  }

}
