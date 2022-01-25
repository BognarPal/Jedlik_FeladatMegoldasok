import { Injectable } from '@nestjs/common';
import { MessageInterface } from '@catch-me/my-ts-lib';

@Injectable()
export class AppService {
  getData(): MessageInterface {
    return {author:"Mr. Developer", message: 'Welcome to api!' };
  }
}
