import { WorkModel } from './../models/work.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ProfessionModel } from './../models/profession.model';
import { EmployeeModel } from './../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  getProfessions(): Observable<ProfessionModel[]> {
    return this.http.get<ProfessionModel[]>('http://munkalap.myddns.me/base/professions')
      .pipe (
        catchError(this.handleError<ProfessionModel[]>('getProfessions', []))
      )
  }

  modifyProfessions(profession: ProfessionModel): Observable<ProfessionModel> {
    return this.http.put<ProfessionModel>('http://munkalap.myddns.me/base/professions', profession)
      .pipe (
        catchError(this.handleError<ProfessionModel>('modifyProfessions', undefined))
      )
  }

  newProfessions(profession: ProfessionModel): Observable<ProfessionModel> {
    return this.http.post<ProfessionModel>('http://munkalap.myddns.me/base/professions', profession)
      .pipe (
        catchError(this.handleError<ProfessionModel>('newProfessions', undefined))
      )
  }

  deleteProfessions(profession: ProfessionModel): Observable<boolean> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }), 
      body: profession
    };

    return this.http.delete<boolean>('http://munkalap.myddns.me/base/professions', httpOptions)
      .pipe (
        catchError(this.handleError<boolean>('deleteProfessions', false))
      )
  }



  getEmployees(): Observable<EmployeeModel[]> {
    return this.http.get<EmployeeModel[]>('http://munkalap.myddns.me/base/employees')
      .pipe (
        catchError(this.handleError<EmployeeModel[]>('getEmployees', []))
      )
  }

  modifyEmployee(employee: EmployeeModel): Observable<EmployeeModel> {
    return this.http.put<EmployeeModel>('http://munkalap.myddns.me/base/employees', employee)
      .pipe (
        catchError(this.handleError<EmployeeModel>('modifyEmployee', undefined))
      )
  }

  newEmployee(employee: EmployeeModel): Observable<EmployeeModel> {
    return this.http.post<EmployeeModel>('http://munkalap.myddns.me/base/employees', employee)
      .pipe (
        catchError(this.handleError<EmployeeModel>('newEmployee', undefined))
      )
  }

  deleteEmployee(employee: EmployeeModel): Observable<boolean> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }), 
      body: employee
    };

    return this.http.delete<boolean>('http://munkalap.myddns.me/base/employees', httpOptions)
      .pipe (
        catchError(this.handleError<boolean>('deleteEmployee', false))
      )
  }


  workList(): Observable<WorkModel[]> {
    return this.http.get<WorkModel[]>('http://munkalap.myddns.me/works/list')
      .pipe (
        catchError(this.handleError<WorkModel[]>('workList', []))
      )
  }
  
  newWork(work: any): Observable<WorkModel> {
    return this.http.post<WorkModel>('http://munkalap.myddns.me/works/new', work)
      .pipe (
        catchError(this.handleError<WorkModel>('newWork', undefined))
      )
  }

  workSetEmployee(work: any): Observable<WorkModel> {
    return this.http.put<WorkModel>('http://munkalap.myddns.me/works/assign', work)
      .pipe (
        catchError(this.handleError<WorkModel>('workSetEmployee', undefined))
      )
  }

  workFinished(work: any): Observable<WorkModel> {
    return this.http.put<WorkModel>('http://munkalap.myddns.me/works/finish', work)
      .pipe (
        catchError(this.handleError<WorkModel>('workFinished', undefined))
      )
  }

  workChecked(work: any): Observable<WorkModel> {
    return this.http.put<WorkModel>('http://munkalap.myddns.me/works/check', work)
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
