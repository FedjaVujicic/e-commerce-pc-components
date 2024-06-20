import { Component } from '@angular/core';
import { UserService } from './shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ComponentShopClient';
  currentUserFirstName: string = "";

  constructor(public userService: UserService, private router: Router) { }

  ngOnInit() {
    this.userService.getUserInfo().subscribe(() => {
      let currentUser = JSON.parse(localStorage.getItem("currentUser"));
      if (currentUser === null) {
        return;
      }
      this.currentUserFirstName = currentUser.firstName;
    });
  }

  signOut() {
    this.userService.signOutUser().subscribe({
      next: () => {
        this.router.navigate([""]);
      },
      error: err => {
        console.log(err);
      }
    });
  }
}
