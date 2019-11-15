import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterViewComponent } from './messagingweb/views/register-view/register-view.component';
import { LoginViewComponent } from './messagingweb/views/login-view/login-view.component';
import { LoginComponent } from './messagingweb/components/login/login.component';
import { RegisterComponent } from './messagingweb/components/register/register.component';
import { UserService } from './messagingweb/services/UserService';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BaseUrlHttpInterceptor } from './messagingweb/services/BaseUrlHttpInterceptor';
import { AuthenticationStore } from './messagingweb/services/AuthenticationStore';
import { AuthenticationHttpInterceptor } from './messagingweb/services/AuthenticationHttpInterceptor';
import { AuthenticationGuardService } from './messagingweb/services/AuthenticationGuardService';
import { HomeViewComponent } from './messagingweb/views/home-view/home-view.component';


@NgModule({
  declarations: [
    AppComponent,
    RegisterViewComponent,
    LoginViewComponent,
    LoginComponent,
    RegisterComponent,
    HomeViewComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    UserService,
    AuthenticationStore,
    AuthenticationGuardService,
    { provide: HTTP_INTERCEPTORS, useClass: BaseUrlHttpInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationHttpInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
