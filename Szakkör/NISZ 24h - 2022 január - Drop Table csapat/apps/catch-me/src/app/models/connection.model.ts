export class ConnectionModel {
    type = 0;
    cityAId = 0;
    cityBId = 0;

    public constructor(init?: Partial<ConnectionModel>) {
        if (init)
            Object.assign(this, init);
    }
}