import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { User } from '../models/user';
import { Router } from '@angular/router';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient, private router: Router) { }

  url: string = `${environment.apiBaseUrl}/Users`;
  formUsername: string;
  formPassword: string;
  currentUser: User;
  loginFailed: boolean = false;

  loginUser() {
    return this.httpClient.post<{ isLoggedIn: boolean, currentUser: User }>(this.url + `/login/?username=${this.formUsername}&password=${this.formPassword}`, {}).pipe(
      tap(res => {
        if (res.isLoggedIn) {
          this.currentUser = res.currentUser as User;
        }
        this.loginFailed = !res.isLoggedIn;
      })
    );

    // return this.httpClient.post<{ isLoggedIn: boolean, currentUser: User }>(this.url + `/login/?username=${this.formUsername}&password=${this.formPassword}`, {}).subscribe({
    //   next: res => {
    //     if (res.isLoggedIn) {
    //       this.currentUser = res.currentUser as User;
    //     }
    //     else {
    //       console.log("Incorrect username or password");
    //     }
    //   },
    //   error: err => {
    //     console.log(err);
    //   }
    // });
  }

  registerUser() {
  }
}
