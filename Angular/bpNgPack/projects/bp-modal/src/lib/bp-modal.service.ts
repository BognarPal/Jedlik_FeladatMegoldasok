import { Injectable, ComponentFactoryResolver, Injector, ApplicationRef, ComponentRef, EmbeddedViewRef } from '@angular/core';
import { BPConfirmComponent } from './components/bp-confirm/bp-confirm.component';

@Injectable({
  providedIn: 'root'
})
export class BPModalService {
  dialogComponentRef: ComponentRef<BPConfirmComponent>;

  constructor(
    private factoryResolver: ComponentFactoryResolver,
    private appRef: ApplicationRef,
    private injector: Injector
  ) { }

  public confirm(
    title: string,
    message: string,
    okText: string = 'OK',
    cancelText: string = 'Mégsem',
    dialogSize: 'sm' | 'md' | 'lg' = 'sm'): Promise<boolean> {
    const factory = this.factoryResolver.resolveComponentFactory(BPConfirmComponent);
    const componentRef = factory.create(this.injector);
    this.appRef.attachView(componentRef.hostView);
    const domElem = (componentRef.hostView as EmbeddedViewRef<any>).rootNodes[0] as HTMLElement;
    document.body.appendChild(domElem);

    this.dialogComponentRef = componentRef;
    componentRef.instance.title = title;
    componentRef.instance.message = message;
    componentRef.instance.okText = okText;
    componentRef.instance.cancelText = cancelText;
    componentRef.instance.size = dialogSize;

    return new Promise((resolve, reject) => {
      componentRef.instance.afterClosed.subscribe(result => {
        componentRef.destroy();
        if (result === null) {
          reject('Dismissed');
        }
        resolve(result);
      }, reject);
    });
  }

}
