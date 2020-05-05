import { Component, OnInit, Input, NgZone, Output, EventEmitter } from '@angular/core';
import { JcmService } from '../jcm.service';
import { BPModalService } from '@bognarpal/bp-modal';
declare var gapi: any;

@Component({
  selector: 'jcm-classroom-card',
  templateUrl: './jcm-classroom-card.component.html',
  styleUrls: ['./jcm-classroom-card.component.css']
})
export class JcmClassroomCardComponent implements OnInit {
  @Input() set course(value) {
    this._course = value;
    this.students = [];
    this.teachers = [];
    setTimeout(() => {
      this.getParticipants();
    }, 100);
  }
  get course() {
    return this._course;
  }
  @Input() get waiting(): boolean {
    return this._waiting;
  }
  @Output() waitingChange = new EventEmitter();
  set waiting(value: boolean) {
    this._waiting = value;
    this.waitingChange.emit(value);
  }

  _waiting = false;
  _course: any = null;
  students: any[] = null;
  teachers: any[] = null;
  errorMessage = '';
  addTeachersDialogVisible = false;
  addStudentsDialogVisible = false;
  teacherInfo = {
    visible: false,
    name: '',
    email: '',
    teacher: null
  };

  studentInfo = {
    visible: false,
    name: '',
    email: '',
    student: null
  };


  constructor(
    private ngZone: NgZone,
    private jcmService: JcmService,
    private bpModalService: BPModalService) { }

  ngOnInit(): void {
  }

  getParticipants() {
    this.students = [];
    this.getStudents(null);

    gapi.client.classroom.courses.teachers.list({
      courseId: this._course.id
    }).then((response: any) => {
      // console.log(response.result);
      this.ngZone.run(() => {
        this.teachers = response.result.teachers.filter(t => t.courseId === this._course.id);
      });
    });
  }

  getStudents(nextToken) {
    gapi.client.classroom.courses.students.list({
      courseId: this._course.id,
      pageSize: 200,
      pageToken: nextToken
    }).then((response: any) => {
      // console.log(response.result);
      if (response.result.students) {
        this.ngZone.run(() => {
          this.students = this.students.concat(response.result.students.filter(s => s.courseId === this._course.id));
        });
        if (response.result.nextPageToken) {
          this.getStudents(response.result.nextPageToken);
        }
      }
    });

  }

  deleteCourse() {
    this.bpModalService.confirm('Kurzus törlése', '<h3 class="text-center">Biztosan törölni szeretné a kurzust?</h3>', 'Igen', 'Nem', 'lg')
      .then((confirmed) => {
        if (confirmed) {
          this.jcmService.courseDelete.emit(this.course);
        }
      })
      .catch(() => console.log('User dismissed the dialog'));
  }

  archiveCourse() {
    this.bpModalService.confirm('Kurzus archiválása', '<h3 class="text-center">Biztosan szeretné archiválni a kurzust?</h3>', 'Igen', 'Nem', 'lg')
      .then((confirmed) => {
        if (confirmed) {
          this.jcmService.courseArchive.emit(this.course);
        }
      })
      .catch(() => console.log('User dismissed the dialog'));
  }

  addTeachers(users: string[]) {
    this.errorMessage = '';
    let count = users.length;
    this.waiting = count !== 0;
    this.addTeachersDialogVisible = count !== 0;

    users.forEach(user => {
      gapi.client.classroom.courses.teachers.create({
        courseId: this._course.id,
        userId: user
      })
        .then((response: any) => {
          // console.log(response.result);
          this.ngZone.run(() => {
            this.teachers.push(response.result);
            this.waiting = --count !== 0;
            this.addTeachersDialogVisible = count !== 0;
          });
        })
        .catch((error) => {
          // console.log(error);
          this.ngZone.run(() => {
            this.errorMessage += (this.errorMessage ? '<br><u>' : '') + '<b><u>' + user + '</u></b>: ' + error.result.error.message;
            this.waiting = --count !== 0;
            this.addTeachersDialogVisible = count !== 0;
          });
        });
    });
  }

  addStudents(users: string[]) {
    this.errorMessage = '';
    let count = users.length;
    this.waiting = count !== 0;
    this.addStudentsDialogVisible = count !== 0;

    users.forEach(user => {
      gapi.client.classroom.courses.students.create({
        courseId: this._course.id,
        userId: user
      })
        .then((response: any) => {
          // console.log(response.result);
          this.ngZone.run(() => {
            this.students.push(response.result);
            this.waiting = --count !== 0;
            this.addStudentsDialogVisible = count !== 0;
          });
        })
        .catch((error) => {
          // console.log(error);
          this.ngZone.run(() => {
            this.errorMessage += (this.errorMessage ? '<br><u>' : '') + '<b><u>' + user + '</u></b>: ' + error.result.error.message;
            this.waiting = --count !== 0;
            this.addStudentsDialogVisible = count !== 0;
          });
        });
    });
  }

  viewTeacherInfo(teacher) {
    this.teacherInfo.teacher = teacher;
    this.teacherInfo.name = teacher.profile.name.familyName + ' ' + teacher.profile.name.givenName;
    this.teacherInfo.email = teacher.profile.emailAddress;
    this.teacherInfo.visible = true;
  }

  viewStudentInfo(student) {
    this.studentInfo.student = student;
    this.studentInfo.name = student.profile.name.familyName + ' ' + student.profile.name.givenName;
    this.studentInfo.email = student.profile.emailAddress;
    this.studentInfo.visible = true;
  }

  deleteTeacher(teacher) {
    this.bpModalService.confirm('Tanár törlése', '<h3 class="text-center">Biztosan törölni szeretné a kurzusból a tanárt?</h3>', 'Igen', 'Nem', 'lg')
      .then((confirmed) => {
        if (confirmed) {
          this.teacherInfo.visible = false;
          this.waiting = true;
          gapi.client.classroom.courses.teachers.delete({
            courseId: this._course.id,
            userId: teacher.email
          })
            .then((response: any) => {
              // console.log(response.result);
              this.ngZone.run(() => {
                const index = this.teachers.findIndex(l => l.profile.emailAddress === teacher.email);
                this.teachers.splice(index, 1);
                this.waiting = false;
              });
            })
            .catch((error) => {
              // console.log(error);
              this.ngZone.run(() => {
                this.errorMessage = error.result.error.message;
                this.waiting = false;
              });
            });
        }
      })
      .catch(() => console.log('User dismissed the dialog'));
  }

  deleteStudent(student) {
    this.bpModalService.confirm('Diák törlése', '<h3 class="text-center">Biztosan törölni szeretné a kurzusból a diákot?</h3>', 'Igen', 'Nem', 'lg')
      .then((confirmed) => {
        if (confirmed) {
          this.studentInfo.visible = false;
          this.waiting = true;
          gapi.client.classroom.courses.students.delete({
            courseId: this._course.id,
            userId: student.email
          })
            .then((response: any) => {
              // console.log(response.result);
              this.ngZone.run(() => {
                const index = this.students.findIndex(l => l.profile.emailAddress === student.email);
                this.students.splice(index, 1);
                this.waiting = false;
              });
            })
            .catch((error) => {
              // console.log(error);
              this.ngZone.run(() => {
                this.errorMessage = error.result.error.message;
                this.waiting = false;
              });
            });
        }
      })
      .catch(() => console.log('User dismissed the dialog'));
  }
}
