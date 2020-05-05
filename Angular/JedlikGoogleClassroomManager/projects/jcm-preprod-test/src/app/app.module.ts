import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { JcmModule } from '@bognarpal/jcm';

import { environment } from '../environments/environment';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    JcmModule.forRoot(environment)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
