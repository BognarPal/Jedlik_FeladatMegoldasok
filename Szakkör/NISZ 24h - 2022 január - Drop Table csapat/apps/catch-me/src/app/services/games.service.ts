import { EventEmitter, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { CityModel } from '../models';

@Injectable({
  providedIn: 'root',
})
export class GamesService {
  public regenerateMap = new EventEmitter();
  public setCurrentCity = new EventEmitter();
  public cities: CityModel[] | undefined;

  constructor(private http: HttpClient) {}

  getAll(): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/lobbys/all`,{});
  }
  
  quitAll(): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/lobbys/quitAll`,{});
  }


  findOne(id:number): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/lobbys/`+id,{});
  }
  
  del(id:number): Observable<any> {
    return this.http.delete<any>(`${environment.apiUrl}/lobbys/`+id,{});
  }

  create(): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/lobbys`,{});
  }
  
  quit(id:number): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/lobbys/quit/`+id,{});
  }
  
  join(id:number): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/lobbys/join/`+id,{});
  }

  getPlayers(): Observable<any[]> {
    return of([
      {
        id: 1,
        name: 'béla',
      },
    ]);
  }
}
