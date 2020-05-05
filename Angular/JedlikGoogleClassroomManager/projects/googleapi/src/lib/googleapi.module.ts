import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule, ModuleWithProviders, Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BPModalModule } from '@bognarpal/bp-modal';

import { GoogleApiService } from './googleapi.service';
import { GoogleUserDirective } from './directives/google-user.directive';
import { GoogleUsersSelectComponent } from './components/google-users-select/google-users-select.component';
import { GoogleUserInformationComponent } from './components/google-user-information/google-user-information.component';


@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    BPModalModule
  ],
  declarations: [
    GoogleUserDirective,
    GoogleUsersSelectComponent,
    GoogleUserInformationComponent
  ],
  exports: [
    GoogleUserDirective,
    GoogleUsersSelectComponent,
    GoogleUserInformationComponent
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class GoogleApiModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: GoogleApiModule,
      providers: [
        GoogleApiService
      ]
    };
  }
}
