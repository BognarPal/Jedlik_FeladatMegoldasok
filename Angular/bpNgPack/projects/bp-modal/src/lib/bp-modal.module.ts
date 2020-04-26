import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { BPModalService } from './bp-modal.service';

import {
  BPModalComponent,
  BPModalHeaderComponent,
  BPModalFooterComponent,
  BPConfirmComponent
} from './components';

@NgModule({
  declarations: [
    BPModalComponent,
    BPModalHeaderComponent,
    BPModalFooterComponent,
    BPConfirmComponent
  ],
  imports: [
    BrowserModule
  ],
  exports: [
    BPModalComponent,
    BPModalHeaderComponent,
    BPModalFooterComponent
  ]
})
export class BPModalModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: BPModalModule,
      providers: [BPModalService]
    };
  }
 }
