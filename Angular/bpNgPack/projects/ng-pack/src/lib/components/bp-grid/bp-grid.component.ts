import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'lib-bp-grid',
  templateUrl: './bp-grid.component.html',
  styleUrls: ['./bp-grid.component.css']
})
export class BpGridComponent implements OnInit {
  @Input() ngStyle: any;
  @Input() ngClass: string;

  constructor() { }

  ngOnInit(): void {
  }

}
