import { NgModule, ModuleWithProviders, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { BPModalModule } from '@bognarpal/bp-modal';
// import { GoogleApiModule } from 'projects/googleapi/src/public-api';
import { GoogleApiModule } from 'googleapi';

import { JcmNewCourseMassSampleComponent } from './components/jcm-new-course-mass-sample.component';
import { JcmComponent } from './components/jcm.component';
import { JcmClassroomCardComponent } from './components/jcm-classroom-card.component';
import { JcmClassroomListComponent } from './components/jcm-classroom-list.component';
import { JcmNewCourseComponent } from './components/jcm-new-course.component';
import { JcmWaitingComponent } from './components/jcm-waiting.component';
import { JcmNewCourseMassComponent } from './components/jcm-new-course-mass.component';

@NgModule({
  declarations: [
    JcmComponent,
    JcmClassroomCardComponent,
    JcmClassroomListComponent,
    JcmNewCourseComponent,
    JcmWaitingComponent,
    JcmNewCourseMassComponent,
    JcmNewCourseMassSampleComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BPModalModule,
    GoogleApiModule.forRoot()
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ],
  exports: [JcmComponent]
})
export class JcmModule {
  public static forRoot(environment: any): ModuleWithProviders<JcmModule> {
    return {
      ngModule: JcmModule,
      providers: [
        {
          provide: 'environment',
          useValue: environment
        }
      ]
    };
  }
}
