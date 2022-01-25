import { PasswordValidatationResultInterface } from './password.validatation.result.interface';

export class RegistrationModel {
    public name?: string;
    public email?: string;
    public password?: string;
    public passwordAgain?: string;

    public constructor(init?: Partial<RegistrationModel>) {
        if (init)
            Object.assign(this, init);
    }

    isPasswordOk(): PasswordValidatationResultInterface {
        if (!this.password || this.password != this.passwordAgain) {
            return {
                ok: false,
                errorCode: 'PASSWORD_MISMATCH',
                error: 'A jelszavak nem egyeznek'
            };
        }
        const strongPassword = new RegExp('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})');
        if (!strongPassword.test(this.password)) {
            return {
                ok: false,
                errorCode: 'PASSWORD_NOT_ENOUGH_COMPLEX',
                error: 'A jelszónak legalább 8 karakternek kell lennie és tartalmaznia kell kis és nagy betűt, számot és speciális karaktert.'
            };
        }
        return {
            ok: true
        };
    }

}