import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { MessageController } from './message.controller';
import { MessageEntity } from './message.entity';
// import { DatabaseModule } from '../database/database.module';
// import { messageProviders } from './message.providers';
// import { MessageService } from './message.service';
import { MessageService } from './message.service';

@Module({
  imports: [TypeOrmModule.forFeature([MessageEntity])],
  providers: [MessageService],
  controllers:[MessageController]
  // providers: [
  //   ...messageProviders,
  //   MessageService,
  // ],
})

// @Module({controllers:[MessageController]})

export class MessageModule {}