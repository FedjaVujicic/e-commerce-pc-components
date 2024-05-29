import { Component } from '@angular/core';
import { UserService } from '../../shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  formEmail: string;
  formPassword: string;
  loginFailed: boolean = false;

  constructor(private router: Router, public userService: UserService) { }

  ngOnInit() {
    this.userService.checkAuthStatus().subscribe({
      next: () => {
        if (this.userService.isLoggedIn) {
          this.router.navigate([""]);
        }
      },
      error: err => {
        console.log(err);
      }
    });
  }

  onSubmit() {
    this.userService.loginUser(this.formEmail, this.formPassword).subscribe({
      next: () => {
        this.router.navigate([""]);
        window.location.reload();
      },
      error: err => {
        this.loginFailed = true;
        console.log(err);
      }
    });
  }
}
