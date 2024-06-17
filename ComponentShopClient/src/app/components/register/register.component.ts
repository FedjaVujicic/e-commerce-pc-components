import { Component } from '@angular/core';
import { UserService } from '../../shared/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { RegistrationDto } from '../../models/registration-dto';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  formEmail: string;
  formFirstName: string;
  formLastName: string;
  formPassword: string;
  formConfirmPassword: string;
  formBirthday: Date;
  formUser: RegistrationDto = new RegistrationDto();
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
    this.createRegistrationDto();
    this.userService.registerUser(this.formUser).subscribe({
      next: () => {
        this.router.navigate(["../login"]);
        this.toastr.success("Success");
      },
      error: err => {
        this.registerFailed = true;
        this.errorMsg = "Registration failed";
        console.log(err);
      }
    });
  }

  createRegistrationDto() {
    this.formUser = {
      email: this.formEmail,
      password: this.formPassword,
      firstName: this.formFirstName,
      lastName: this.formLastName,
      birthday: this.formBirthday
    };
  }
}
