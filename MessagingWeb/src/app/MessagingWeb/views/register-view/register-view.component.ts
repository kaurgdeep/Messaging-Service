import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IRegisterUser } from '../../Dtos/Interfaces/IRegisterUser';
import { UserService } from '../../services/UserService';


@Component({
  selector: 'app-register-view',
  templateUrl: './register-view.component.html',
  styleUrls: ['./register-view.component.scss']
})
export class RegisterViewComponent implements OnInit {

  apiCall: boolean;
  errorStatus: string;
 

  constructor(private activatedRoute: ActivatedRoute, public router: Router, private userService: UserService ) { }

  ngOnInit() {

  }

  async register(user: IRegisterUser) {
    const response = await this.userService.create(user);
    console.log(response);
    if (response) {
      //this.status = 'Login succeeded! Redirecting to home page';
      setTimeout(() => {
        console.log('Redirecting to home', JSON.stringify(localStorage));
        this.router.navigate(['/home']);
      }, 100);
    } else {
      this.apiCall = false;
      this.errorStatus = 'Login failed';
    }

    
    
  }

}
