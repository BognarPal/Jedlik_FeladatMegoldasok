export class LoginResponseModel {
    email?: string;
    name?: string;
    roles?: string[];
    token?: string;
    validTo?: Date;

    public constructor(init?:Partial<LoginResponseModel>) {
        if (init)
            Object.assign(this, init);
    }

}