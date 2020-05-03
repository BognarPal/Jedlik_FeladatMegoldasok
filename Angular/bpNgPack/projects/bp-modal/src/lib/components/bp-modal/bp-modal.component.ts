import { Component, Input, Output, EventEmitter, ElementRef, Renderer2, AfterViewChecked, HostListener } from '@angular/core';

@Component({
  selector: 'bp-modal-header',
  template: '<ng-content></ng-content>'
})
export class BPModalHeaderComponent { }

@Component({
  selector: 'bp-modal-footer',
  template: '<ng-content></ng-content>'
})
export class BPModalFooterComponent { }


@Component({
  selector: 'bp-modal',
  templateUrl: './bp-modal.component.html',
  styleUrls: ['./bp-modal.component.css']
})
export class BPModalComponent implements AfterViewChecked {
  @Input() style: any;
  @Input() contentStyle: any;
  @Input() closeButton = true;
  @Input() get visible(): boolean {
    return this._visible;
  }
  @Output() visibleChange = new EventEmitter();
  set visible(value: boolean) {
    this._visible = value;
    this.visibleChange.emit(value);
    this.viewChecked = false;
    if (this._visible) {
      this.modalSettings.zIndexFade = this.maxZIndex() + 1;
      this.modalSettings.zIndexModal = this.modalSettings.zIndexFade + 1;
    }
  }

  _visible = false;
  viewChecked = false;
  modalSettings = {
    zIndexFade: 0,
    zIndexModal: 0,
    dragging: false,
    lastPageX: 0,
    lastPageY: 0,
    width: 0,
    height: 0,
    left: 0,
    top: 0
  };

  constructor(
    private elementRef: ElementRef,
    private renderer: Renderer2) {
  }

  ngAfterViewChecked(): void {
    if (this.visible && !this.viewChecked) {
      this.modalSettings.dragging = false;

      const element = this.elementRef.nativeElement.querySelector('.modal-content');
      this.modalSettings.width = element.offsetWidth;
      this.modalSettings.height = element.offsetHeight;
      this.modalSettings.left = (window.innerWidth - this.modalSettings.width) / 2;
      this.modalSettings.top = (window.innerHeight - this.modalSettings.height) / 2;

      this.renderer.setStyle(element, 'top', this.modalSettings.top.toString() + 'px');
      this.renderer.setStyle(element, 'left', this.modalSettings.left.toString() + 'px');

      this.viewChecked = true;
    }
  }

  dismiss() {
    this.visible = false;
  }

  initDrag(event: MouseEvent) {
    if (event.view.innerWidth > this.modalSettings.width && event.view.innerHeight > this.modalSettings.height) {
      this.modalSettings.dragging = true;
      this.modalSettings.lastPageX = event.pageX;
      this.modalSettings.lastPageY = event.pageY;
    }
  }

  @HostListener('document:mousemove', ['$event']) mouseMove(event: MouseEvent) {
    if (this.modalSettings.dragging && this.visible) {
      if (event.buttons !== 1) {
        this.modalSettings.dragging = false;
      } else {

        const left = this.modalSettings.left + event.pageX - this.modalSettings.lastPageX;
        const top = this.modalSettings.top + event.pageY - this.modalSettings.lastPageY;

        if (left < 0) {
          this.modalSettings.left = 0;
        } else if (this.modalSettings.left + this.modalSettings.width > window.innerWidth) {
          this.modalSettings.left = window.innerWidth - this.modalSettings.width;
        } else {
          this.modalSettings.left = left;
        }

        if (top < 0) {
          this.modalSettings.top = 0;
        } else if (this.modalSettings.top + this.modalSettings.height > window.innerHeight) {
          this.modalSettings.top = window.innerHeight - this.modalSettings.height;
        } else {
          this.modalSettings.top = top;
        }

        const element = this.elementRef.nativeElement.querySelector('.modal-content');
        this.renderer.setStyle(element, 'top', this.modalSettings.top.toString() + 'px');
        this.renderer.setStyle(element, 'left', this.modalSettings.left.toString() + 'px');

        this.modalSettings.lastPageX = event.pageX;
        this.modalSettings.lastPageY = event.pageY;
      }
    }
  }

  @HostListener('document:mouseup') mouseUp() {
    this.modalSettings.dragging = false;
  }

  @HostListener('window:resize') resize() {
    this.modalSettings.dragging = false;
    if (this.visible) {
      this.viewChecked = false;
      this.ngAfterViewChecked();
    }
  }

  maxZIndex() {
    const maxIndex =  Array.from(document.querySelectorAll('body *'))
      .filter(a => !a.classList.contains('app-waiting'))
      .map(a => parseFloat(window.getComputedStyle(a).zIndex))
      .filter(a => !isNaN(a))
      .sort((a, b) => Number(a) - Number(b))
      .pop();
    return maxIndex < 0 ? 0 : maxIndex;
  }

}
