import { SetMetadata, UseGuards } from '@nestjs/common';
import { RolesGuard } from './roles.guard';
import { applyDecorators } from '@nestjs/common';

export const Roles = (...roles: string[]) => applyDecorators(
    SetMetadata('roles', roles),
    UseGuards(RolesGuard)
);
