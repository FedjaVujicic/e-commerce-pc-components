import { Component } from '@angular/core';
import { UserService } from './shared/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ComponentShopClient';
  currentUserFirstName: string = "";

  constructor(public userService: UserService) { }

  ngOnInit() {
    this.userService.getUserInfo().subscribe(() => {
      let currentUser = JSON.parse(localStorage.getItem("currentUser"));
      this.currentUserFirstName = currentUser.firstName;
    });
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
