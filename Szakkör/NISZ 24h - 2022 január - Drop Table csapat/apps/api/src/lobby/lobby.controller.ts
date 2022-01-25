import { Body, Controller, Delete, Get, Param, Post, Headers } from '@nestjs/common';
import { LobbyService } from './lobby.service';
import { Roles } from '../auth/roles.decorator';

@Controller('lobbys')
export class LobbyController {
  constructor(private lobbyService: LobbyService) {}

  @Post()
  @Roles()
  async create( @Headers() headers) {
    const token = headers["Authorization"] || headers["authorization"];
    if (token) {
          return this.lobbyService.create(token.toString());
        }
        else{ 
          return false;
        }
  }

  @Get("all")
  @Roles()
  async all() {
    return this.lobbyService.all();
  }
  
  @Get(":id")
  @Roles()
  async find(@Param('id') id: number) {
    return this.lobbyService.find(id);
  }

  @Delete(':id')
  @Roles()
  async delete(
    @Param('id') id: number, @Headers() headers
    ) {
      const token = headers["Authorization"] || headers["authorization"];
        if (token) {
          return this.lobbyService.delete(id,token.toString());
        }
        else{ 
          return false;
        }
  }

  @Post("join/:id")
  @Roles()
  async join(@Param('id') id: number,@Headers() headers) {
    const token = headers["Authorization"] || headers["authorization"];
        if (token) {
          return this.lobbyService.join(id,token);
        }
        else{ 
          return false;
        }
  }

  
  @Post("quit/:id")
  @Roles()
  async quit(@Param('id') id: number,@Headers() headers) {
    const token = headers["Authorization"] || headers["authorization"];
        if (token) {
          return this.lobbyService.quit(id,token);
        }
        else{ 
          return false;
        }
  }
  
  @Post("quitAll")
  @Roles()
  async quitAll(@Headers() headers) {
    const token = headers["Authorization"] || headers["authorization"];
        if (token) {
          return this.lobbyService.quitAll(token);
        }
        else{ 
          return false;
        }
  }
}
