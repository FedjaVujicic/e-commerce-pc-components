import { Component } from '@angular/core';
import { UserService } from './shared/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ComponentShopClient';

  constructor(public userService: UserService) { }

  ngOnInit() {
    this.userService.getUserInfo().subscribe();
  }

  isAdmin() {
    if (!this.userService.isLoggedIn) {
      return false;
    }
    let role = JSON.parse(localStorage.getItem("currentUser")).role;
    if (role != "Admin") {
      return false;
    }
    return true;
  }

  signOut() {
    this.userService.signOutUser().subscribe({
      next: () => {
        window.location.reload();
      },
      error: err => {
        console.log(err);
      }
    });
  }
}
