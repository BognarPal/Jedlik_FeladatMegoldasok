import { HttpStatus } from "@nestjs/common";

export type OperationExceptionMessage =
    | 'UNKNOWN_ERROR'
    | 'EMAIL_IN_USE'
    | 'NOT_FOUND'
    | 'INVALID_EMAIL_OR_PASSWORD'
    | 'INVALID_TOKEN'
    | 'NOT_AUTHORIZED'
    | 'EMAIL_ADDRESS_ALREADY_IN_USE'
    | 'PASSWORD_MISMATCH'
    | 'PASSWORD_NOT_ENOUGH_COMPLEX'
    | 'INVALID_LOBBY_ROLE'
    | 'INVALID_BODY'

export class OperationException extends Error {
    constructor(message: OperationExceptionMessage, public readonly status: HttpStatus) {
        super(message);
    }
}


