import { ExceptionFilter, Catch, ArgumentsHost } from '@nestjs/common';
import { Request, Response } from 'express';
import { OperationException } from './operation-exception';

@Catch(OperationException)
export class OperationExceptionFilter implements ExceptionFilter {
  catch(exception: OperationException, host: ArgumentsHost) {
    const ctx = host.switchToHttp();
    const response = ctx.getResponse<Response>();
    const request = ctx.getRequest<Request>();
    console.warn(`Caught Operational Error for ${request.path}:`, exception.message, exception.stack);
    const status = exception.status;

    response
      .status(status)
      .json({
        message:
          exception.message == 'INVALID_EMAIL_OR_PASSWORD' ? 'Hibás felhasználónév vagy jelszó' :
            (exception.message == 'EMAIL_ADDRESS_ALREADY_IN_USE' ? 'Az e-mail cím már használatban van' :
              (exception.message == 'PASSWORD_MISMATCH' ? 'A jelszavak nem egyeznek' :
                (exception.message == 'INVALID_ROLE' ? 'Nincs megfelelő lobby jogosultság!' :
                  (exception.message == 'INVALID_BODY' ? 'Nem megfelelő kérés!' :
                    (exception.message == 'PASSWORD_NOT_ENOUGH_COMPLEX' ? 'A jelszó nem elég komplex' : exception.message)))))
      });
  }
}