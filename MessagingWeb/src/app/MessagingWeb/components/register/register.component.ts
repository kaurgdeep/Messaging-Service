import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IRegisterUser } from '../../Dtos/Interfaces/IRegisterUser';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent implements OnInit {

  @Input() emailAddress: string;
  @Input() name: string;
  @Input() busy: boolean;

  @Input() errorStatus: string;
  @Input() status: string;

  @Output() registerClick = new EventEmitter<IRegisterUser>();
  
  vm: IRegisterUser = { };
  

  constructor() { }

  isDefined(o: any) {
      return (typeof (o) !== 'undefined');
  }

  ngOnInit() {
      this.vm.emailAddress = this.emailAddress;
      this.vm.name = this.name;
  }

  register() {
      this.registerClick.emit({ 
      name: this.vm.name,
      emailAddress: this.vm.emailAddress, 
      password: this.vm.password,
    });
  }

  

}
