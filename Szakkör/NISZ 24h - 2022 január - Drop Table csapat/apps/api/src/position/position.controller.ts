import { Body, Controller, Delete, Get, Param, Post, Headers } from '@nestjs/common';
import { PositionInterface } from './position.interface';
import { PositionService } from './position.service';
import { Roles } from '../auth/roles.decorator';

@Controller('positions')
export class PositionController {
  constructor(private positionService: PositionService) {}

  @Post("create")
  @Roles()
  async create( @Headers() headers,@Body() body: PositionInterface) {
    const token = headers["Authorization"] || headers["authorization"];
    if (token) {
          return this.positionService.create(token.toString(),body.turn,body.used_vehicle,body.playground_id);
        }
        else{ 
          return false;
        }
  }


  @Post("get/:id")
  @Roles()
  async getAllByPlayGroundId( @Headers() headers,@Param('id') id: number) {
    const token = headers["Authorization"] || headers["authorization"];
    if (token) {
          return this.positionService.getAllByPlayGroundId(token.toString(),id);
        }
        else{ 
          return false;
        }
  }
  
}
