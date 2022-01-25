import { Body, Controller, Get, Headers, Post } from '@nestjs/common';
import { LoginInterface, LoginResponseModel, RegistrationModel } from '@catch-me/my-ts-lib';
import { AuthService } from './auth.service';
import { Roles } from './roles.decorator';

@Controller("auth")
export class AuthController {

    constructor(private authService: AuthService) { }

    @Post("login")
    public async login(@Body() model: LoginInterface): Promise<LoginResponseModel> {
        return await this.authService.login(model);
    }

    @Get("checktoken")
    @Roles()
    public async CheckToken(): Promise<boolean> {
        return true;
    }

    @Post("logout")
    public async Logout(@Headers() headers): Promise<boolean> {
        const token = headers["Authorization"] || headers["authorization"];
        if (token) {
            return await this.authService.deleteSession(token.toString());
        }
        return true;
    }

    @Post("registration")
    public async regisztration(@Body() model: RegistrationModel): Promise<LoginResponseModel> {
        return await this.authService.registerUser(model);
    }

}