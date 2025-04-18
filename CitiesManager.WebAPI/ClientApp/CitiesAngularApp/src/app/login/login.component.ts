import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterUser } from '../models/register-user';
import { AccountService } from '../services/account.service';
import { CompareValidation } from '../validators/custom-validators';
import { LoginUser } from '../models/login-user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  loginForm: FormGroup;
  isLoginFormSubmitted: boolean = false;

  constructor(private accountService: AccountService, private router: Router) {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required])
    });
  }

  get login_emailControl(): any {
    return this.loginForm.controls["email"];
  }
  get login_passwordControl(): any {
    return this.loginForm.controls["password"];
  }

  loginSubmitted() {
    this.isLoginFormSubmitted = true;
    if (this.loginForm.valid) {
      this.accountService.postLogin(this.loginForm.value).subscribe({
        next: (response: any) => {
          console.log(response);

          this.isLoginFormSubmitted = false;
          this.accountService.currentUserName = response.email;
          localStorage["token"] = response.token;

          this.router.navigate(['/cities']);

          this.loginForm.reset();
        },

        error: (error) => {
          console.log(error)
        },

        complete: () => { }
      });
    }
  }
}
