import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: []
})
export class ConfirmComponent implements OnInit {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter();
  @Output() confirmed = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  confirm(): void {
    this.visible = false;
    this.confirmed.emit();
  }

  dismiss() {
    this.visible = false;
    this.visibleChange.emit();
  }

}
