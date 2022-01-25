import { AuthModule } from './../auth/auth.module';
import { MiddlewareConsumer, Module, NestModule } from '@nestjs/common';
import { ConfigModule } from '@nestjs/config';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { MessageModule } from '../message/message.module';
import { MessageEntity } from '../message/message.entity';
import { SessionEntity } from '../auth/session.entity';
import { RoleEntity } from '../auth/role.entity';
import { UserEntity } from '../auth/user.entity';
import { AuthService } from '../auth/auth.service';
import { UserService } from '../auth/user.service';
import { RoleService } from '../auth/role.service';
import { AuthController } from '../auth/auth.controller';
import { UserController } from '../auth/user.controller';
import { LastAccessedMiddleWare } from '../auth/lastaccessed.middleware';
import { LobbyEntity } from '../lobby/lobby.entity';
import { LobbyController } from '../lobby/lobby.controller';
import { LobbyService } from '../lobby/lobby.service';
import { MapController } from '../lobby/map.conroller';
import { PlaygroundEntity } from '../playground/playground.entity';
import { PositionEntity } from '../position/position.entity';
import { PlaygroundController } from '../playground/playground.controller';
import { PositionController } from '../position/position.controller';
import { PositionService } from '../position/position.service';
import { PlaygroundService } from '../playground/playground.service';

@Module({
  imports: [
    ConfigModule.forRoot(),
    TypeOrmModule.forRoot({
      type: 'mysql',
      host: process.env.DATABASE_HOST || 'localhost',
      port: Number(process.env.DATABASE_PORT || '3306'),
      username: process.env.DATABASE_USER || 'root',
      password: process.env.DATABASE_PASSWORD || '',
      database: process.env.DATABASE_NAME || 'myDatabase',
      entities: [
        MessageEntity,
        UserEntity,
        RoleEntity,
        SessionEntity,
        LobbyEntity,
        PositionEntity,
        PlaygroundEntity
      ],
      //autoLoadEntities:true, //only in development mode
      synchronize: true,
    }),

    TypeOrmModule.forFeature([SessionEntity, RoleEntity, UserEntity,LobbyEntity,PlaygroundEntity,PositionEntity]),
    MessageModule,
    // GameModule,
    // AuthModule,
  ],
    controllers: [AppController, AuthController, UserController, MapController, LobbyController,PositionController,PlaygroundController],
    providers: [AuthService, UserService, RoleService, AppService, LobbyService,PositionService ,PlaygroundService],
})
export class AppModule implements NestModule {
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
          }
          userService.getByEmail('admin@droptable.jedlik.local').then(async usr => {
          if (!usr) {
            await this.createAdminUser();
          }
        });
      })
  }

  private async createAdminUser() {
      const user = new UserEntity();
      user.name = 'admin';
      user.email = 'admin@droptable.jedlik.local';
      user.passwordHash = UserService.hashPassword('admin');
      user.roles = await this.roleService.find({ where: { name: 'admin' } });
      console.log(user);
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