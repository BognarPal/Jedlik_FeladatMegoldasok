import { OperationExceptionMessage } from "../errorhandling";

export interface PasswordValidatationResultInterface {
    ok: boolean;
    errorCode?: OperationExceptionMessage;
    error?: string
}

