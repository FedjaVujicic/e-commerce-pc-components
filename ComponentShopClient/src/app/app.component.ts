import { Component } from '@angular/core';
import { UserService } from './shared/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ComponentShopClient';
  isLoggedIn = false;

  constructor(public userService: UserService) { }

  ngOnInit() {
    this.userService.checkAuthStatus().subscribe({
      next: () => { },
      error: err => {
        console.log(err);
      }
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
