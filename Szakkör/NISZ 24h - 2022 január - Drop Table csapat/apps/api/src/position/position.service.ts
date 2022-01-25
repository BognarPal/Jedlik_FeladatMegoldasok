import { HttpStatus, Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { PositionEntity } from './position.entity';
import { AuthService } from '../auth/auth.service';
import { UserEntity } from '../auth/user.entity';
import { SessionEntity } from '../auth/session.entity';
import { OperationException } from 'libs/my-ts-lib/src/errorhandling';
import { PositionInterface } from './position.interface';

@Injectable()
export class PositionService {
  constructor(
     @InjectRepository(PositionEntity)
     private readonly positionRepository: Repository<PositionEntity>,
     private authService: AuthService
  ) {}

  async create(token,turn, used_vehicle, playground_id): Promise<PositionInterface> {
    const session: SessionEntity = await this.authService.getSession(token);
    if (!session.id) {
      throw new OperationException('INVALID_BODY', HttpStatus.BAD_REQUEST);
    }
    return this.positionRepository.save({user_id:session.user.id,turn:turn,used_vehicle:used_vehicle, playground_id:playground_id});
  }
  

  async getAllByPlayGroundId(token,playground_id): Promise<PositionInterface[]> {
    const session: SessionEntity = await this.authService.getSession(token);
    if (!session.id) {
      throw new OperationException('INVALID_BODY', HttpStatus.BAD_REQUEST);
    }
    const rt = (await this.positionRepository.find({where: { playground_id: playground_id }})).map(a => {
      const backk:PositionInterface = {user_id: a.user_id,playground_id: a.playground_id,turn: a.turn,used_vehicle: a.used_vehicle}
      return backk;
    });
    return rt;
  }

}
