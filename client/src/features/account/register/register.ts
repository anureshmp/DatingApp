import { Component, input, output } from '@angular/core';
import { RegisterCreds } from '../../../types/registercreds';
import { FormsModule } from '@angular/forms';
import { User } from '../../../types/user';

@Component({
  imports: [FormsModule],
  selector: 'app-register',
  styleUrl: './register.css',
  templateUrl: './register.html',
})
export class Register {
  usersFromHome = input.required<User[]>();
  protected creds = {} as RegisterCreds;
  cancelRegister = output<boolean>();

  register() {
    console.log(this.creds);
  }

  cancel() {
    this.cancelRegister.emit(false);
  }


}
