export class CityModel {
    name = '';
    id = 0;
    x = 0;
    y = 0;

    public constructor(init?: Partial<CityModel>) {
        if (init)
            Object.assign(this, init);
    }
}