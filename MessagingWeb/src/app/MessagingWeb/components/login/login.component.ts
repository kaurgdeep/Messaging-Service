import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ILoginUser } from '../../Dtos/Interfaces/ILoginUser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  @Input() emailAddress: string;
  @Input() busy: boolean;

  @Input() errorStatus: string;
  @Input() status: string;
  @Output() loginClick = new EventEmitter<ILoginUser>();

  vm: ILoginUser = {};
  constructor() { }

  isDefined(o: any) {
    return (typeof (o) !== 'undefined');
}

ngOnInit() {
    this.vm.emailAddress = this.emailAddress;
}

  login() {
    this.loginClick.emit({ 
    emailAddress: this.vm.emailAddress, 
    password: this.vm.password,
    });
  }

}
