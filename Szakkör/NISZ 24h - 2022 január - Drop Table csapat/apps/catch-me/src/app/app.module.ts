import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {
  AppComponent,
  LoginComponent,
  RegistrationComponent,
  HomeComponent,
  MapContainerComponent,
  MapComponent,
  GamesComponent,
  LobbyComponent,
} from './components';
import { AppRoutingModule } from './app-routing.module';
import { MeetComponent } from './components/meet/meet.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AuthGuard } from './auth.guard';
import { GameComponent } from './components/game/game.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    HomeComponent,
    RegistrationComponent,
    GamesComponent,
    MeetComponent,
    MapContainerComponent,
    MapComponent,
    LobbyComponent,
    GameComponent,
  ],
  imports: [BrowserModule, HttpClientModule, FormsModule, AppRoutingModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    AuthGuard,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
