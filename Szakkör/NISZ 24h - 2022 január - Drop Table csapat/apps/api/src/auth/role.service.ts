import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { GenericService } from '../generics/generic.service';
import { RoleEntity } from './role.entity';

@Injectable()
export class RoleService extends GenericService<RoleEntity>{
    constructor(@InjectRepository(RoleEntity) userRepository: Repository<RoleEntity>) {
        super(userRepository);
    }
}


