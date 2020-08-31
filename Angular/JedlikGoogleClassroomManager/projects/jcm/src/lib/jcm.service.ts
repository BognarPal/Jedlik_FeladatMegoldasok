import { Injectable, EventEmitter } from '@angular/core';
declare var gapi: any;

@Injectable({
  providedIn: 'root'
})
export class JcmService {

  public newCourseAdded = new EventEmitter();
  public courseDelete = new EventEmitter();
  public courseArchive = new EventEmitter();
  public courseRename = new EventEmitter();
  constructor() { }

  public newCourse(name, owner): Promise<any> {
    return gapi.client.classroom.courses.create({
      name,
      ownerId: owner,
      courseState: 'ACTIVE'
    }).then((response: any) => {
      this.newCourseAdded.emit(response.result);
      return {
        result: response.result,
        errorMsg: ''
      };
    }).catch((error) => {
      return {
        errorMsg: error.result.error.message
      };
    });
  }

}
