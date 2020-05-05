import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { JcmModule } from 'projects/jcm/src/public-api';
import { GoogleApiModule } from 'projects/googleapi/src/public-api';

import { environment } from './../environments/environment';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    JcmModule.forRoot(environment),
    GoogleApiModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
