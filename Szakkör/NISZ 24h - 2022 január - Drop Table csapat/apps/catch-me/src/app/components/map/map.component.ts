import { Component, OnInit, ChangeDetectionStrategy, Input, ElementRef } from '@angular/core';
import Map from 'ol/Map';

@Component({
  selector: 'catch-me-map',
  template: '',
  styles: [':host { width: 100%; height: 100%; display: block; }'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MapComponent implements OnInit {
  @Input() map!: Map;

  constructor(private elementRef: ElementRef) {
  }

  ngOnInit() {
    this.map.setTarget(this.elementRef.nativeElement);
  }

 

}
