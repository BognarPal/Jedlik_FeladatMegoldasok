import { Component, EventEmitter } from '@angular/core';

@Component({
  selector: 'bp-confirm',
  templateUrl: './bp-confirm.component.html'
})
export class BPConfirmComponent {

  title: string;
  message: string;
  okText: string;
  cancelText: string;
  size = 'sm';
  afterClosed = new EventEmitter();

  constructor() { }

  public decline() {
    this.afterClosed.emit(false);
  }

  public accept() {
    this.afterClosed.emit(true);
  }

  public dismiss() {
    this.afterClosed.emit(null);
  }



}
