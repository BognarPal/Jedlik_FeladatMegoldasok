import { Injectable, EventEmitter, NgZone } from '@angular/core';
declare var gapi: any;

@Injectable({
  providedIn: 'root'
})
export class JcmService {

  public newCourseAdded = new EventEmitter();
  public courseDelete = new EventEmitter();
  public courseArchive = new EventEmitter();
  public courseRename = new EventEmitter();

  constructor(private ngZone: NgZone) { }

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

  public getStudents(courseid, students, nextToken): Promise<any> {
    return gapi.client.classroom.courses.students.list({
      courseId: courseid,
      pageSize: 200,
      pageToken: nextToken
    }).then((response: any) => {
      if (response.result.students) {
        this.ngZone.run(() => {
          students = students.concat(response.result.students.filter(s => s.courseId === courseid));
        });
        if (response.result.nextPageToken) {
          return this.getStudents(courseid, students, response.result.nextPageToken);
        } else {
          return students;
        }
      } else {
        return [];
      }
    }).catch((error) => {
      return [];
    });
  }

}
