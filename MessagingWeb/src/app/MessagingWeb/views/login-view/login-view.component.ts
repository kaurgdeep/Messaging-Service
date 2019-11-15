import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/UserService';
import { ILoginUser } from '../../Dtos/Interfaces/ILoginUser';

@Component({
  selector: 'app-login-view',
  templateUrl: './login-view.component.html',
  styleUrls: ['./login-view.component.scss']
})
export class LoginViewComponent implements OnInit {

  apiCall: boolean;
  errorStatus: string;
  status: string;

  constructor(private activatedRoute: ActivatedRoute, public router: Router, private userService: UserService ) { }

  ngOnInit() {
    this.userService.logout();
  }

  async login(user: ILoginUser) {
    console.log(user);
    this.apiCall = true;
    const response = await this.userService.login(user);
    if (response) {
      this.status = 'Login succeeded! Redirecting to home page';
      setTimeout(() => {
        console.log('Redirecting to home', JSON.stringify(localStorage));
        this.router.navigate(['/home']);
      }, 100);
    } else {
      this.apiCall = false;
      this.errorStatus = 'Login failed';
    }
    //console.log(user);
  }

    
    
  

}
