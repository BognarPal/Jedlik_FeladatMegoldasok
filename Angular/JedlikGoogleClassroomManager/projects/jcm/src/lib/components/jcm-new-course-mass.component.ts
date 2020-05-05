import { Component, OnInit, Input, Output, EventEmitter, NgZone } from '@angular/core';
import { GoogleApiService } from 'googleapi';
import { JcmService } from '../jcm.service';
declare var gapi: any;

@Component({
  selector: 'jcm-new-course-mass',
  templateUrl: './jcm-new-course-mass.component.html',
  styleUrls: ['./jcm-new-course-mass.component.css']
})
export class JcmNewCourseMassComponent implements OnInit {
  @Input() errorMessage = '';
  @Input() get visible(): boolean {
    return this._visible;
  }
  @Output() visibleChange = new EventEmitter();
  @Output() viewSample = new EventEmitter();
  set visible(value: boolean) {
    this._visible = value;
    this.visibleChange.emit(value);
  }

  _visible = false;
  isFileOverZone = false;
  sampleVisible = false;
  fileReader = new FileReader();
  courses = [];
  openedCourse = null;
  saving = false;

  constructor(
    private ngZone: NgZone,
    private googleApiService: GoogleApiService,
    private jcmService: JcmService) { }

  ngOnInit(): void {
    this.fileReader.onload = (e) => {
      this.parseFile(this.fileReader.result);
    };
  }

  cancel() {
    this.visible = false;
    this.courses = [];
  }

  filezoneDrop(event) {
    // Prevent default behavior(file from being opened)
    event.preventDefault();
    this.isFileOverZone = false;

    if (event.dataTransfer.items && event.dataTransfer.items.length === 1 && event.dataTransfer.items[0].kind === 'file') {
      const file = event.dataTransfer.items[0].getAsFile();
      this.fileReader.readAsText(file, 'utf-8');
    }
  }

  filezoneDragover(event) {
    event.preventDefault();
    event.stopPropagation();
    this.isFileOverZone = true;
  }

  filezoneDragleave(event) {
    event.preventDefault();
    event.stopPropagation();
    this.isFileOverZone = false;
  }

  fileBrowsed(event) {
    if (event.target.files && event.target.files.length === 1) {
      const file = event.target.files[0];
      this.fileReader.readAsText(file, 'utf-8');
    }
  }

  parseFile(text: any) {
    this.courses = [];
    const array = text.split('\r\n');
    array.splice(0, 1);
    array.forEach(element => {
      const columns = element.split('\t');
      if (columns.length === 4 && columns[0]) {
        const course = {
          name: columns[0],
          owner: columns[1],
          teachers: columns[2],
          students: columns[3],
          status: 0,
          checked: false,
          checkedStatus: 0,
          checkedTeachers: [],
          failedTeachers: [],
          checkedStudents: [],
          failedStudents: [],
          hasError: false
        };
        this.courses.push(course);
        this.checkCourseData(course);
      }
    });
  }

  checkCourseData(course: any) {
    this.checkEmail(course.owner).then((result) => {
      if (result === 'USER') {
        this.ngZone.run(() => {
          course.checkedOwner = course.owner;
        });
      } else {
        course.hasError = true;
      }
      course.checked = ++course.checkedStatus >= 3;
    });

    let teacherCount = 0;
    course.teachers.split(';').filter(t => t.toUpperCase() !== course.owner.toUpperCase()).forEach(teacher => {
      teacherCount++;
      this.checkEmail(teacher).then((result) => {
        if (result === 'USER') {
          this.ngZone.run(() => {
            course.checkedTeachers.push(teacher);
            if (--teacherCount === 0) {
              course.checked = ++course.checkedStatus >= 3;
            }
          });
        } else if (result === '') {
          this.ngZone.run(() => {
            course.failedTeachers.push(teacher);
            course.hasError = true;
            if (--teacherCount === 0) {
              course.checked = ++course.checkedStatus >= 3;
            }
          });
        } else {
          this.googleApiService.groupMembersEmail({ email: teacher }).then((addresses) => {
            this.ngZone.run(() => {
              addresses.filter(a => a.toUpperCase() !== course.owner.toUpperCase()).forEach(address => {
                course.checkedTeachers.push(address);
              });
              if (--teacherCount === 0) {
                course.checked = ++course.checkedStatus >= 3;
              }
            });
          });
        }
      });
    });

    let studentCount = 0;
    course.students.split(';').filter(s => s.toUpperCase() !== course.owner.toUpperCase()).forEach(student => {
      studentCount++;
      this.checkEmail(student).then((result) => {
        if (result === 'USER') {
          this.ngZone.run(() => {
            course.checkedStudents.push(student);
            if (--studentCount === 0) {
              course.checked = ++course.checkedStatus >= 3;
            }
          });
        } else if (result === '') {
          this.ngZone.run(() => {
            course.failedStudents.push(student);
            course.hasError = true;
            if (--studentCount === 0) {
              course.checked = ++course.checkedStatus >= 3;
            }
          });
        } else {
          this.googleApiService.groupMembersEmail({ email: student }).then((addresses) => {
            this.ngZone.run(() => {
              addresses.filter(a => a.toUpperCase() !== course.owner.toUpperCase()).forEach(address => {
                course.checkedStudents.push(address);
              });
              if (--studentCount === 0) {
                course.checked = ++course.checkedStatus >= 3;
              }
            });
          });
        }
      });
    });
  }


  checkEmail(email: string): Promise<string> {
    return gapi.client.directory.users.list({
      customer: 'my_customer',
      maxResults: 20,
      orderby: 'email',
      query: 'email:' + email
    }).then((response: any) => {
      const users = response.result.users || [];
      if (users.length === 1) {
        return 'USER';
      } else {
        return gapi.client.directory.groups.list({
          customer: 'my_customer',
          maxResults: 20,
          orderby: 'email',
          query: 'email:*' + email + '*'
        }).then((response2: any) => {
          const groups = response2.result.groups || [];
          if (groups.length === 1 && groups[0].email === email) {
            return 'GROUP';
          } else {
            return '';
          }
        });
      }
    });
  }


  openCourse(course) {
    if (!this.openedCourse) {
      this.openedCourse = course;
    } else if (this.openedCourse.name === course.name) {
      this.openedCourse = null;
    } else {
      this.openedCourse = course;
    }

  }

  save() {
    let ok = true;
    this.courses.forEach(course => ok = ok && !course.hasError && course.checked);
    if (ok) {
      this.openedCourse = null;
      this.saving = true;
      this.courses.forEach(course => {
        this.jcmService.newCourse(course.name, course.owner).then((result) => {
          this.ngZone.run(() => {
            course.status += 20;
          });
          console.log(result);
          const teacherCount = course.checkedTeachers.length;
          course.checkedTeachers.forEach(teacher => {
            gapi.client.classroom.courses.teachers.create({
              courseId: result.result.id,
              userId: teacher
            }).then((response: any) => {
              this.ngZone.run(() => {
                course.status += (40 / teacherCount);
              });
            }).catch(() => {
              this.ngZone.run(() => {
                course.status += (40 / teacherCount);
              });
            });
          });


          const studentCount = course.checkedStudents.length;
          course.checkedStudents.forEach(student => {
            gapi.client.classroom.courses.students.create({
              courseId: result.result.id,
              userId: student
            }).then((response: any) => {
              this.ngZone.run(() => {
                course.status += (40 / studentCount);
              });
            }).catch(() => {
              this.ngZone.run(() => {
                course.status += (40 / teacherCount);
              });
            });
          });
        });
      });

      setTimeout(() => this.isSaveReady(), 500);

    }
  }
  isSaveReady(): void {
    let ready = true;
    this.courses.forEach(course => ready = ready && Math.round(course.status) === 100);
    if (ready) {
      this.saving = false;
      this.visible = false;
    } else {
      setTimeout(() => this.isSaveReady(), 500);
    }
  }
}
