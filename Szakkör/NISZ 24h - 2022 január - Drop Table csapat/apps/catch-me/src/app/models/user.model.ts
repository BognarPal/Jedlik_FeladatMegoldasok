export class UserModel{
    name = '';
    email = '';
    roles: string[] = [];
    token?: string;
    validTo?: Date;

    loadFromLocalStorage(): void {
        const usrJson = localStorage.getItem('currentUser');
        if (usrJson) {
            const usr = JSON.parse(usrJson);
            if (usr) {
                this.email = usr.name;
                try {
                    this.name = decodeURIComponent(usr.name);
                } catch {
                    this.name = usr.name;
                }
                this.roles = usr.roles;
                this.token = usr.token;
                this.validTo = new Date(usr.validTo);                
            }
        }
    }
}
