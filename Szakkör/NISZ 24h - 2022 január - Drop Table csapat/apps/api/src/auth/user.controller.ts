import { Controller, Get, Param } from '@nestjs/common';
import { UserInterface } from '@catch-me/my-ts-lib';
import { Roles } from './roles.decorator';
import { UserService } from './user.service';
  
  @Controller("users")
  export class UserController{
    
    constructor(private userService: UserService) { }
      
    @Get(":id")
    @Roles("admin")
    public async getUserById(@Param() id: number): Promise<UserInterface> {        
        return await this.userService.getById(id)
    }
  
    @Get()
    @Roles("admin")
    public async getAllUsers(): Promise<UserInterface[]> {
        return await this.userService.getAll();
    }
  }