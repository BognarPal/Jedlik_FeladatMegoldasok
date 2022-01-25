import { AuthService } from './auth.service';

import { Injectable, NestMiddleware } from '@nestjs/common';
import { Request, Response, NextFunction } from 'express';
import { LoginResponseModel } from '@catch-me/my-ts-lib';

@Injectable()
export class LastAccessedMiddleWare implements NestMiddleware {

    constructor(private authService: AuthService) { }

    async use(request: Request, response: Response, next: NextFunction) {
        const token = request.headers["Authorization"] || request.headers["authorization"];
        if (token) {
            const session = await this.authService.updateSessionLastAccessDate(token.toString());
            if (session) {
                const validTo = new Date()
                validTo.setTime(session.lastAccess.getTime() + parseInt(process.env.SESSION_TIMEOUT_MINUTE as string, 10) * 60 * 1000);
                response.header('access-control-expose-headers', 'session');
                response.header('session', JSON.stringify(new LoginResponseModel({
                    email: session.user.email,
                    name: session.user.name,
                    roles: session.user.roles.map(r => r.name),
                    token: session.token,
                    validTo: validTo
                })));
            }
        }
        next();
    }
}
