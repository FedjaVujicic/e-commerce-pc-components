import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { User } from '../../models/user';
import { UserService } from '../../shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register-requests',
  templateUrl: './register-requests.component.html',
  styleUrl: './register-requests.component.css'
})
export class RegisterRequestsComponent {

  users: Array<User> = [];

  constructor(private http: HttpClient, public userService: UserService, private toastr: ToastrService) { }

  ngOnInit() {
    this.userService.getUsersForApproval().subscribe(res => {
      this.users = res as Array<User>;
    });
  }

  putUserStatus(username: string, status: string) {
    this.userService.putUserStatus(username, status).subscribe(() => {
      this.toastr.success("Success!");
      this.userService.getUsersForApproval().subscribe(res => {
        this.users = res as Array<User>;
      });
    });
  }

}
