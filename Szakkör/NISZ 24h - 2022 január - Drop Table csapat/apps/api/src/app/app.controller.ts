import { Controller, Get } from '@nestjs/common';

import { MessageInterface } from '@catch-me/my-ts-lib';

import { AppService } from './app.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {
  }

  // @Get('hello')
  // getData(): MessageInterface {
  //   return this.appService.getData();
  // }

  
}
