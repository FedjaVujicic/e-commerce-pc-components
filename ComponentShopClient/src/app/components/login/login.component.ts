import { Component } from '@angular/core';
import { UserService } from '../../shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginErrorMsg: string = "Incorrect username or password";

  constructor(private router: Router, public userService: UserService) { }

  onSubmit() {
    this.userService.loginUser().subscribe({
      next: res => {
        if (!this.userService.loginFailed) {
          // Save user session
          // Navigate to home page
          this.router.navigate(["../edit-monitors"]);
        }
      },
      error: err => {
        console.log(err);
      }
    });
  }
}
