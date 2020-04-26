import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BPModalModule } from 'projects/bp-modal/src/public-api';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BPModalModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
