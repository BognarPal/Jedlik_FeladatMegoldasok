import { AuthService } from './auth.service';
import { CanActivate, ExecutionContext, Injectable } from '@nestjs/common';
import { Observable } from 'rxjs';
import { Reflector } from '@nestjs/core';


@Injectable()
export class RolesGuard implements CanActivate {

    constructor(private reflector: Reflector, private authService: AuthService) { }

    canActivate(context: ExecutionContext): boolean | Promise<boolean> | Observable<boolean> {
        const roles = this.reflector.get<string[]>('roles', context.getHandler());
        const request = context.switchToHttp().getRequest();
        const token = request.headers["Authorization"] || request.headers["authorization"];
        if (!token) {
            return false;
        }
        else {
            return this.authService.getSession(token.toString()).then(session => {
                if (!session) {
                    return false;
                } else {
                    const validTo = new Date()
                    validTo.setTime(session.lastAccess.getTime() + parseInt(process.env.SESSION_TIMEOUT_MINUTE as string, 10) * 60 * 1000);
                    if (validTo < new Date()) {
                        return false;
                    }
                    const matchedRoles = session.user.roles.filter(r => roles?.includes(r.name));
                    if (matchedRoles.length > 0 || !roles || roles.length == 0) {
                        return true;
                    }
                    return false;
                }
            });
        }
    }
}

