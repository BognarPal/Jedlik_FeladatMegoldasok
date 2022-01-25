import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { MessageInterface } from '@catch-me/my-ts-lib';
import { Repository } from 'typeorm';
import { MessageEntity } from './message.entity';

@Injectable()
export class MessageService {
  constructor(
    @InjectRepository(MessageEntity) private readonly messageRepository: Repository<MessageEntity>
  ) {
  
  }

  async all():Promise<MessageEntity[]> {
    return this.messageRepository.find();
  }

  
  async create(data):Promise<MessageEntity> {
    return this.messageRepository.save(data);
  }
  
  async get(id:number):Promise<MessageEntity> {
    return this.messageRepository.findOne(id);
  }

  async update(id: number, data: MessageInterface): Promise<any>{
    return this.messageRepository.update(id,data)
  }
  
  async delete(id: number): Promise<any>{
    return this.messageRepository.delete(id)
  }

  async getRandom(): Promise<MessageEntity>{
    const messages: MessageEntity[] = await this.messageRepository.find();
    return messages[Math.floor(Math.random() * messages.length)];
  }
  
  async getRandomMessage(): Promise<MessageInterface> {
    const messages: MessageEntity[] = await this.messageRepository.find();
    const myMessage = messages[Math.floor(Math.random() * messages.length)];
    return {author:myMessage.author, message: myMessage.message};
  }
}

