import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import * as bcrypt from 'bcrypt';
import { UserEntity } from './user.entity';
import { GenericService } from '../generics/generic.service';

@Injectable()
export class UserService extends GenericService<UserEntity>{
    constructor(@InjectRepository(UserEntity) userRepository: Repository<UserEntity>) {
        super(userRepository);
    }

    async getByEmail(email: string): Promise<UserEntity | undefined> {
        const user = await this.repository.findOne({
            where: { email: email },
            relations: ['roles']
        });
        return user;
    }

    async insert(model: UserEntity): Promise<UserEntity> {
        await this.repository.insert(model)
        const user = this.getByEmail(model.email);
        return user as Promise<UserEntity>;
    }

    static hashPassword(password: string): string {
        return bcrypt.hashSync(password, 10);
    }
}


