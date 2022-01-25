import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from '../../environments/environment';
import { CityModel } from '../models';
import { ConnectionModel } from '../models/connection.model';

@Injectable({
  providedIn: 'root',
})
export class MapService {
  constructor(private http: HttpClient) {}

  public getCities(): Observable<CityModel[]> {
    return this.http.get<CityModel[]>(`${environment.apiUrl}/map/all`, {});
  }

  public getConnections(): Observable<ConnectionModel[]> {
    return this.http.get<ConnectionModel[]>(`${environment.apiUrl}/map/con`, {});
  }
}
