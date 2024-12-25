import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { Credentials } from '../model/credentials.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private userService: UserService) { }

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  login(): void {
    var cred: Credentials = {
      email: this.loginForm.value.email || "",
      password: this.loginForm.value.password || "",
    };

    this.userService.login(cred).subscribe(

    )
  }
}
