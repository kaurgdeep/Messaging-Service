import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterViewComponent } from './messagingweb/views/register-view/register-view.component';
import { LoginViewComponent } from './messagingweb/views/login-view/login-view.component';
import { AuthenticationGuardService } from './messagingweb/services/AuthenticationGuardService';
import { HomeViewComponent } from './messagingweb/views/home-view/home-view.component';




const routes: Routes = [
  { path: '', component: RegisterViewComponent },
  { path: 'login', component: LoginViewComponent },
  { path: 'register', component: RegisterViewComponent },
  { path: 'home', component: HomeViewComponent, canActivate: [AuthenticationGuardService] },


  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
