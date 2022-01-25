import { HttpStatus, Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import * as bcrypt from 'bcrypt';
import { GenericService } from '../generics/generic.service';
import { SessionEntity } from './session.entity';
import { LoginInterface, LoginResponseModel, OperationException, RegistrationModel } from '@catch-me/my-ts-lib';
import { UserService } from './user.service';
import { UserEntity } from './user.entity';

@Injectable()
export class AuthService extends GenericService<SessionEntity> {
    constructor(
        @InjectRepository(SessionEntity) sessionRepository: Repository<SessionEntity>,
        private userService: UserService,
    ) {
        super(sessionRepository);
    }

    async login(model: LoginInterface): Promise<LoginResponseModel> {
        const user = await this.userService.getByEmail(model.email);
        if (!user || !bcrypt.compareSync(model.password, user.passwordHash)) {
            throw new OperationException('INVALID_EMAIL_OR_PASSWORD', HttpStatus.FORBIDDEN);
        }
        return this.generateSession(user);
    }

    async getSession(token: string): Promise<SessionEntity | undefined> {
        const session = await this.repository.findOne({
            where: { token: token },
            relations: ['user', 'user.roles']
        });

        if (session) {
            const validTo = new Date()
            validTo.setTime(session.lastAccess.getTime() + parseInt((process.env.SESSION_TIMEOUT_MINUTE || '20') as string, 10) * 60 * 1000);
            if (validTo < new Date()) {
                console.log('expired token - deleted');
                await this.repository.remove(session);
                return undefined;
            }
        }
        return session;
    };

    async deleteSession(token: string): Promise<boolean> {
        const session = await this.repository.findOne({
            where: { token: token }
        });

        if (session) {
            await this.repository.remove(session);
        }
        return true;
    };

    async updateSessionLastAccessDate(token: string): Promise<SessionEntity | undefined> {
        const session = await this.getSession(token);
        if (session) {
            session.lastAccess = new Date();
            await this.repository.save(session)
            return session;
        }
        return undefined;
    }

    generateToken(length = 120): string {
        let token = '';
        while (token.length < length)
            token += Math.random().toString(36).slice(2);
        return token.substring(0, length);
    }

    async registerUser(model: RegistrationModel): Promise<LoginResponseModel> {
        if (await this.userService.getByEmail(model.email)) {
            throw new OperationException('EMAIL_ADDRESS_ALREADY_IN_USE', HttpStatus.BAD_REQUEST);
        }
        const reg = new RegistrationModel(model);
        const passwordCheck = reg.isPasswordOk();
        if (!passwordCheck.ok) {
            throw new OperationException(passwordCheck.errorCode, HttpStatus.BAD_REQUEST)
        }

        let user = new UserEntity();
        user.name = reg.name;
        user.email = reg.email;
        user.passwordHash = await UserService.hashPassword(reg.password);
        user = await this.userService.insert(user);

        return this.generateSession(user);
    }

    private async generateSession(user: UserEntity): Promise<LoginResponseModel> {
        const session = new SessionEntity({
            user: user,
            lastAccess: new Date(),
            token: this.generateToken()
        });
        await this.repository.insert(session)
        const validTo = new Date()
        validTo.setTime(session.lastAccess.getTime() + parseInt(process.env.SESSION_TIMEOUT_MINUTE as string, 10) * 60 * 1000);
        const response = new LoginResponseModel({
            email: user.email,
            name: user.name,
            roles: user.roles.map(r => r.name),
            token: session.token,
            validTo: validTo
        });
        return response;
    }

}