import { Component } from '@angular/core';
import { UserService } from '../../shared/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  formUsername: string;
  formPassword: string;
  formConfirmPassword: string;
  registerFailed: boolean = false;

  errorMsg: string = "";

  constructor(public userService: UserService, private toastr: ToastrService, private router: Router) { }

  validateForm(): boolean {
    if (this.formPassword != this.formConfirmPassword) {
      this.errorMsg = "Passwords do not match";
      return false;
    }

    return true;
  }

  onSubmit(): void {
    if (!this.validateForm()) {
      this.registerFailed = true;
      return;
    }
    this.userService.registerUser(this.formUsername, this.formPassword).subscribe({
      next: () => {
        this.router.navigate(["../login"]);
        this.toastr.success("Success");
      },
      error: err => {
        this.registerFailed = true;
        this.errorMsg = "Username already in use";
        console.log(err);
      }
    });
  }
}
