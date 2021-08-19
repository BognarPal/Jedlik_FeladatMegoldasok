import { Component, OnInit, AfterContentInit, NgZone, Inject, Input } from '@angular/core';
import { GoogleApiService } from '@jedlik/googleapi';
import { JcmService } from '../jcm.service';
declare var gapi: any;

@Component({
  // tslint:disable-next-line: component-selector
  selector: 'jcm',
  templateUrl: './jcm.component.html',
  styleUrls: ['./jcm.component.css']
})
export class JcmComponent implements OnInit, AfterContentInit {
  @Input() listHeight = 'calc(100vh - 8.5rem)';

  DISCOVERY_DOCS = [
    'https://www.googleapis.com/discovery/v1/apis/classroom/v1/rest',
    'https://www.googleapis.com/discovery/v1/apis/admin/directory_v1/rest'
  ];
  SCOPES =
    'https://www.googleapis.com/auth/classroom.courses ' +
    'https://www.googleapis.com/auth/classroom.rosters ' +
    'https://www.googleapis.com/auth/classroom.profile.emails ' +
    'https://www.googleapis.com/auth/classroom.profile.photos ' +
    'https://www.googleapis.com/auth/classroom.topics ' +
    'https://www.googleapis.com/auth/admin.directory.user.readonly ' +
    'https://www.googleapis.com/auth/admin.directory.user ' +
    'https://www.googleapis.com/auth/admin.directory.group.readonly ' +
    'https://www.googleapis.com/auth/admin.directory.group ' +
    'https://www.googleapis.com/auth/admin.directory.group.member.readonly ' +
    'https://www.googleapis.com/auth/admin.directory.group.member ' +
    'https://www.googleapis.com/auth/cloud-platform';

  isSignedIn = false;
  profile = null;
  courses = [];
  selectedCourse = null;
  isWaiting = false;
  sampleCSVVisible = false;

  newCourse = {
    modalVisible: false,
    errorMessage: ''
  };

  newCourseMass = {
    modalVisible: false
  };

  constructor(
    private ngZone: NgZone,
    private googleApiService: GoogleApiService,
    private jcmService: JcmService,
    @Inject('environment') private environment) { }

  ngOnInit(): void {
    this.jcmService.courseDelete.subscribe((course) => this.deleteCourse(course));
    this.jcmService.courseArchive.subscribe((course) => this.archiveCourse(course));
    this.jcmService.courseRename.subscribe((course) => this.renameCourse(course));
  }

  ngAfterContentInit(): void {
    if (this.googleApiService.apiLoaded) {
      this.initGapi();
    } else {
      this.googleApiService.apiLoadedChange.subscribe(
        (success: boolean) => {
          if (success) {
            this.initGapi();
          } else {
            console.log('Error loading Google API');
          }
        }
      );
    }
  }

  initGapi() {
    gapi.load('client:auth2', () => {
      gapi.client.init({
        apiKey: this.environment.API_KEY,
        clientId: this.environment.CLIENT_ID,
        discoveryDocs: this.DISCOVERY_DOCS,
        scope: this.SCOPES
      }).then(() => {
        // Listen for sign-in state changes.
        gapi.auth2.getAuthInstance().isSignedIn.listen(status => this.updateSigninStatus(status));

        // Handle the initial sign-in state.
        this.updateSigninStatus(gapi.auth2.getAuthInstance().isSignedIn.get());
      },
        (error) => {
          console.log(JSON.stringify(error, null, 2));
        });
    });
  }

  updateSigninStatus(isSignedIn) {
    setTimeout(() => {
      this.ngZone.run(() => {
        this.isSignedIn = isSignedIn;
      });
    }, 100);
    this.profile = null;
    if (isSignedIn) {
      this.profile = gapi.auth2.getAuthInstance().currentUser.get().getBasicProfile();
      this.listCourses(null);
    }
  }

  login() {
    gapi.auth2.getAuthInstance().signIn();
  }

  logout() {
    gapi.auth2.getAuthInstance().signOut();
    this.courses = [];
    this.selectedCourse = null;
  }

  listCourses(token) {
    if (!token) {
      this.courses = [];
      this.selectedCourse = null;
    }
    gapi.client.classroom.courses.list({
      pageSize: 100,
      pageToken: token
    }).then((response: any) => {
      this.ngZone.run(() => {
        this.courses = this.courses.concat(response.result.courses);
      });
      if (response.result.nextPageToken) {
        this.listCourses(response.result.nextPageToken);
      } else {
        this.ngZone.run(() => {
          this.courses.forEach(course => {
            course.numberOfStudents = null;
            course.lastTopicTime = null;
            if (course.courseState === 'ACTIVE') {
              this.jcmService.getStudents(course.id, [], null).then((students) => {
                if (students != null) {
                  course.numberOfStudents = students.length;
                } else {
                  course.numberOfStudents = undefined;
                }
              });
            }
          });
        });
      }
      setTimeout(() => {
        this.ngZone.run(() => this.readMissingNumberOfStudents(this.ngZone));
      }, 30000);
    });
  }

  readMissingNumberOfStudents(ngZone: NgZone) {
    let any = false;
    this.courses.filter(c => c.numberOfStudents === undefined && c.courseState === 'ACTIVE').forEach(course => {
      this.jcmService.getStudents(course.id, [], null).then((students) => {
        if (students != null) {
          course.numberOfStudents = students.length;
        } else {
          course.numberOfStudents = undefined;
        }
      });
      any = true;
    });
    if (any) {
      setTimeout(() => {
        ngZone.run(() => this.readMissingNumberOfStudents(ngZone));
      }, 30000);
    }
  }

  selectCourse(course: any) {
    this.selectedCourse = course;
  }

  addCourse() {
    this.newCourse.modalVisible = true;
  }

  addCourseFromTxt() {
    this.newCourseMass.modalVisible = true;
  }

  saveNewCourse(course) {
    this.newCourse.errorMessage = '';
    this.isWaiting = true;
    this.jcmService.newCourse(course.name, course.owner).then((response) => {
      this.isWaiting = false;
      this.newCourse.modalVisible = false;
      this.courses.unshift(response.result);
      this.selectedCourse = response.result;
    });
  }

  deleteCourse(course) {
    this.isWaiting = true;
    gapi.client.classroom.courses.delete({
      id: course.id
    }).then((response: any) => {
      this.isWaiting = false;
      const index = this.courses.findIndex(c => c.id === course.id);
      this.courses.splice(index, 1);
      this.selectedCourse = null;
    });
  }

  archiveCourse(course) {
    this.isWaiting = true;
    gapi.client.classroom.courses.update({
      id: course.id,
      courseState: 'ARCHIVED',
      name: course.name
    }).then((response: any) => {
      this.isWaiting = false;
      const index = this.courses.findIndex(c => c.id === course.id);
      this.courses[index].courseState = response.result.courseState;
    });
  }

  renameCourse(course) {
    this.isWaiting = true;
    gapi.client.classroom.courses.update({
      id: course.id,
      name: course.name,
      courseState: course.courseState,
    }).then((response: any) => {
      this.isWaiting = false;
      const index = this.courses.findIndex(c => c.id === course.id);
      this.courses[index].courseState = response.result.courseState;
    });
  }

}
