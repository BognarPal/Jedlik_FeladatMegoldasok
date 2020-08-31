import { Component, OnInit, Input, NgZone, Output, EventEmitter } from '@angular/core';
import { JcmService } from '../jcm.service';
declare var gapi: any;

@Component({
  selector: 'jcm-classroom-list',
  templateUrl: './jcm-classroom-list.component.html',
  styleUrls: ['./jcm-classroom-list.component.css']
})
export class JcmClassroomListComponent implements OnInit {
  @Input() height = 'calc(100vh - 8.5rem)';
  @Output() selectCourse = new EventEmitter();
  @Input() set courses(values: any[]) {
    this._courses = values;
    setTimeout(() => {
      this.getOwners();
    }, 100);
  }
  owners = [];
  statuses = [];
  _courses = [];
  _filter = {
    name: '',
    state: 'ACTIVE',
    order: 'name'
  };

  set filter(value) {
    this._filter = value;
  }

  get filter() {
    return this._filter;
  }

  get courses() {
    return this._courses;
  }

  get filteredCourses() {
    let courses = this._courses;
    if (this.filter.name) {
      courses = courses.filter(c => c.name.toUpperCase().includes(this.filter.name.toUpperCase()));
    }
    if (this.filter.state) {
      courses = courses.filter(c => c.courseState === this.filter.state);
    }
    if (this.filter.order.substr(0, 4) === 'name') {
      courses.sort((a, b) => {
        const value = a.name > b.name ? 1 : -1;
        return this.filter.order.indexOf('!') === -1 ? value : -value;
      });
    } else if (this.filter.order.substr(0, 12) === 'creationTime') {
      courses.sort((a, b) => {
        const value = a.creationTime > b.creationTime ? 1 : -1;
        return this.filter.order.indexOf('!') === -1 ? value : -value;
      });
    } else if (this.filter.order.substr(0, 9) === 'ownerName') {
      courses.sort((a, b) => {
        const value = a.ownerName > b.ownerName ? 1 : -1;
        return this.filter.order.indexOf('!') === -1 ? value : -value;
      });
    }

    return courses;
  }

  constructor(private ngZone: NgZone, private jcmService: JcmService) { }

  ngOnInit(): void {
    this.jcmService.newCourseAdded.subscribe((course) => this.getOwners());
  }

  select(course: any) {
    this.selectCourse.emit(course);
  }

  filterByName(filter: string) {
    console.log(filter);
  }

  getOwners() {
    this._courses.forEach(course => {
      if (this.statuses.filter(s => s === course.courseState).length === 0) {
        this.statuses.push(course.courseState);
      }
    });

    this._courses.forEach(course => {
      if (this.owners.filter(o => o.id === course.ownerId).length === 0) {
        this.owners.push({
          id: course.ownerId,
          name: ''
        });
      }
    });
    this.owners.forEach(owner => {
      gapi.client.directory.users.get({
        userKey: owner.id
      }).then((response: any) => {
        // console.log(response.result);
        owner.name = response.result.name.familyName + ' ' + response.result.name.givenName;
        this.ngZone.run(() => {
          this._courses.filter(o => o.ownerId === owner.id).forEach(course => {
            course.ownerName = owner.name;
            course.ownerPhotoUrl = response.result.thumbnailPhotoUrl;
          });
        });
      });
    });
  }

  orderBy(fieldName) {
    if (this.filter.order.indexOf(fieldName) === -1) {
      this.filter.order = fieldName;
    } else
      if (this.filter.order.indexOf('!') === -1) {
        this.filter.order = fieldName + '!';
      } else {
        this.filter.order = fieldName;
      }
  }
}
