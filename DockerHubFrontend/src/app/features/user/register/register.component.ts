import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { User } from '../model/user-model.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registrationForm: FormGroup = new FormGroup({});

  constructor(private formBuilder: FormBuilder, private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.registrationForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
        ],
      ],
      confirmPassword: ['', Validators.required],
      name: ['', Validators.required],
      surname: ['', Validators.required]
    });
  }

  register(): void {
    if (
      this.registrationForm.value.password !==
      this.registrationForm.value.confirmPassword
    ) {
      alert('Passwords do not match');
    } else {
      let user: User = {
        id: 0,
        email: this.registrationForm.value.email,
        password: this.registrationForm.value.password,
        username: this.registrationForm.value.name,
        role: 0
      };

      this.userService.register(user).subscribe((response) => {
        if (response) {
          console.log('Succesfully registered.');
          console.log(response);
          this.router.navigate(['/home'])
        }
      });
    }
  }
  
}
