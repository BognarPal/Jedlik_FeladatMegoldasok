import { WorkModel } from './../models/work.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ProfessionModel } from './../models/profession.model';
import { EmployeeModel } from './../models/employee.model';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  getProfessions(): Observable<ProfessionModel[]> {
    return this.http.get<ProfessionModel[]>(`${environment.apiURL}/base/professions`)
      .pipe (
        catchError(this.handleError<ProfessionModel[]>('getProfessions', []))
      )
  }

  modifyProfessions(profession: ProfessionModel): Observable<ProfessionModel> {
    return this.http.put<ProfessionModel>(`${environment.apiURL}/base/professions`, profession)
      .pipe (
        catchError(this.handleError<ProfessionModel>('modifyProfessions', undefined))
      )
  }

  newProfessions(profession: ProfessionModel): Observable<ProfessionModel> {
    return this.http.post<ProfessionModel>(`${environment.apiURL}/base/professions`, profession)
      .pipe (
        catchError(this.handleError<ProfessionModel>('newProfessions', undefined))
      )
  }

  deleteProfessions(profession: ProfessionModel): Observable<boolean> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }), 
      body: profession
    };

    return this.http.delete<boolean>(`${environment.apiURL}/base/professions`, httpOptions)
      .pipe (
        catchError(this.handleError<boolean>('deleteProfessions', false))
      )
  }



  getEmployees(): Observable<EmployeeModel[]> {
    return this.http.get<EmployeeModel[]>(`${environment.apiURL}/base/employees`)
      .pipe (
        catchError(this.handleError<EmployeeModel[]>('getEmployees', []))
      )
  }

  modifyEmployee(employee: EmployeeModel): Observable<EmployeeModel> {
    return this.http.put<EmployeeModel>(`${environment.apiURL}/base/employees`, employee)
      .pipe (
        catchError(this.handleError<EmployeeModel>('modifyEmployee', undefined))
      )
  }

  newEmployee(employee: EmployeeModel): Observable<EmployeeModel> {
    return this.http.post<EmployeeModel>(`${environment.apiURL}/base/employees`, employee)
      .pipe (
        catchError(this.handleError<EmployeeModel>('newEmployee', undefined))
      )
  }

  deleteEmployee(employee: EmployeeModel): Observable<boolean> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }), 
      body: employee
    };

    return this.http.delete<boolean>(`${environment.apiURL}/base/employees`, httpOptions)
      .pipe (
        catchError(this.handleError<boolean>('deleteEmployee', false))
      )
  }


  workList(): Observable<WorkModel[]> {
    return this.http.get<WorkModel[]>(`${environment.apiURL}/works/list`)
      .pipe (
        catchError(this.handleError<WorkModel[]>('workList', []))
      )
  }
  
  newWork(work: any): Observable<WorkModel> {
    return this.http.post<WorkModel>(`${environment.apiURL}/works/new`, work)
      .pipe (
        catchError(this.handleError<WorkModel>('newWork', undefined))
      )
  }

  workSetEmployee(work: any): Observable<WorkModel> {
    return this.http.put<WorkModel>(`${environment.apiURL}/works/assign`, work)
      .pipe (
        catchError(this.handleError<WorkModel>('workSetEmployee', undefined))
      )
  }

  workFinished(work: any): Observable<WorkModel> {
    return this.http.put<WorkModel>(`${environment.apiURL}/works/finish`, work)
      .pipe (
        catchError(this.handleError<WorkModel>('workFinished', undefined))
      )
  }

  workChecked(work: any): Observable<WorkModel> {
    return this.http.put<WorkModel>(`${environment.apiURL}/works/check`, work)
      .pipe (
        catchError(this.handleError<WorkModel>('workChecked', undefined))
      )
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // TODO: better job of transforming error for user consumption
      // this.log(`${operation} failed: ${error.message}`);
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
