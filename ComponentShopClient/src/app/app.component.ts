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

  signOut() {
    this.userService.signOutUser().subscribe(() => { console.log("Success"); });
  }
}
