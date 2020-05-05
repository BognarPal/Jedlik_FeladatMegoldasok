import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'jcm-new-course',
  templateUrl: './jcm-new-course.component.html',
  styleUrls: ['./jcm-new-course.component.css']
})
export class JcmNewCourseComponent implements OnInit {
  @Input() errorMessage = '';
  @Input() get visible(): boolean {
    return this._visible;
  }
  @Input() profile = null;
  @Output() visibleChange = new EventEmitter();
  @Output() newCourse = new EventEmitter();
  set visible(value: boolean) {
    this.name = '';
    this.owner = this.profile ? this.profile.getEmail() : '';
    this._visible = value;
    this.visibleChange.emit(value);
  }

  _visible = false;
  name = '';
  owner = '';
  querying = false;

  constructor() { }

  ngOnInit(): void {
  }

  save() {
    if (this.name && this.owner) {
      this.newCourse.emit({
        name: this.name,
        owner: this.owner
      });
    }
  }

}
