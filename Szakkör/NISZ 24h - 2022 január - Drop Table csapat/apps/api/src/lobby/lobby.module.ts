import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { LobbyController } from './lobby.controller';
import { LobbyEntity } from './lobby.entity';
import { LobbyService } from './lobby.service';

@Module({
  imports: [TypeOrmModule.forFeature([LobbyEntity])],
  providers: [LobbyService],
  controllers:[LobbyController]
})

export class LobbyModule {}