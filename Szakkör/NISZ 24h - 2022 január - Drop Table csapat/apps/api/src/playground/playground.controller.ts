import { Body, Controller, Delete, Get, Param, Post, Headers } from '@nestjs/common';
import { PlaygroundService } from './playground.service';
import { Roles } from '../auth/roles.decorator';

@Controller('playgrounds')
export class PlaygroundController {
  constructor(private playgroundService: PlaygroundService) {}

  @Post("create")
  @Roles()
  async create( @Headers() headers) {
    const token = headers["Authorization"] || headers["authorization"];
    if (token) {
          return this.playgroundService.create(token.toString());
        }
        else{ 
          return false;
        }
  }
  @Post("end")
  @Roles()
  async endGame( @Headers() headers) {
    const token = headers["Authorization"] || headers["authorization"];
    if (token) {
          return this.playgroundService.endGame(token.toString());
        }
        else{ 
          return false;
        }
  }
  @Post("next_turn")
  @Roles()
  async next( @Headers() headers) {
    const token = headers["Authorization"] || headers["authorization"];
    if (token) {
          return this.playgroundService.next(token.toString());
        }
        else{ 
          return false;
        }
  }

}
