import { MiddlewareConsumer, Module, NestModule } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AuthController } from './auth.controller';
import { AuthService } from './auth.service';
import { LastAccessedMiddleWare } from './lastaccessed.middleware';
import { RoleEntity } from './role.entity';
import { RoleService } from './role.service';
import { SessionEntity } from './session.entity';
import { UserController } from './user.controller';
import { UserEntity } from './user.entity';
import { UserService } from './user.service';

 @Module({
//     imports: [
//         TypeOrmModule.forFeature([
//             SessionEntity,
//             RoleEntity,
//             UserEntity
//         ])
//     ],
//     providers: [
//         AuthService,
//         UserService,
//         RoleService
//     ],
//     controllers: [
//         AuthController,
//         UserController
//     ]
 })

export class AuthModule implements NestModule {
    configure(consumer: MiddlewareConsumer) {
        consumer
            .apply(LastAccessedMiddleWare)
            .forRoutes('*');
    }

    constructor(
        private userService: UserService,
        private roleService: RoleService) {

        roleService.find({ where: { name: 'admin' } }).then(async roles => {
            if (roles.length == 0) {
                await this.createRoles();
                userService.getByEmail('admin@droptable.jedlik.local').then(async usr => {
                    if (!usr) {
                        await this.createAdminUser();
                    }
                });
            }
        })
    }

    private async createAdminUser() {
        const user = new UserEntity();
        user.name = 'admin';
        user.email = 'admin@droptable.jedlik.local';
        user.passwordHash = UserService.hashPassword('admin');
        user.roles = await this.roleService.find({ where: { name: 'admin' } });
        await this.userService.insert(user);
    }

    private async createRoles() {
        const role = new RoleEntity();
        role.name = 'admin';
        await this.roleService.create(role);
        
        // role = new RoleEntity();
        // role.name = 'lobbyAdmin';
        // await this.roleService.create(role);
    }
}