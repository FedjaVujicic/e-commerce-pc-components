import { Component } from '@angular/core';
import { UserService } from '../../shared/user.service';
import { User } from '../../models/user';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {

  user: User = new User();
  isChangingDetails: boolean = false;
  isChangingPassword: boolean = false;
  currentPassword: string = "";
  newPassword: string = "";
  confirmPassword: string = "";

  constructor(public userService: UserService, private toastr: ToastrService) { }

  ngOnInit() {
    this.userService.getUserInfo().subscribe(() => {
      let currentUser = JSON.parse(localStorage.getItem("currentUser"));
      if (currentUser === null) {
        return;
      }
      this.user = {
        username: currentUser.username,
        role: currentUser.role,
        firstName: currentUser.firstName,
        lastName: currentUser.lastName,
        birthday: new Date(currentUser.birthday),
        credits: currentUser.credits,
      };
    });
  }

  showDetailsForm() {
    this.isChangingDetails = true;
  }

  hideDetailsForm() {
    this.isChangingDetails = false;
  }

  showPasswordForm() {
    this.isChangingPassword = true;
  }

  hidePasswordForm() {
    this.isChangingPassword = false;
  }

  onSubmit() {
    this.userService.putUser(this.user).subscribe(() => {
      this.toastr.success("Success");
    });
  }

  changePassword() {
    this.userService.changePassword(this.currentPassword, this.newPassword).subscribe(() => {
      this.toastr.success("Success");
    });
  }
}
