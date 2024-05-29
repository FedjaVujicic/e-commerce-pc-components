import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { Router } from '@angular/router';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private router: Router) { }

  url: string = `https://localhost:7142`;
  isLoggedIn = false;

  loginUser(formEmail: string, formPassword: string) {
    return this.http.post(this.url + `/login/?useCookies=true`,
      {
        email: formEmail,
        password: formPassword,
      }, { withCredentials: true });
  }

  registerUser(formEmail: string, formPassword: string) {
    return this.http.post(this.url + `/register`,
      {
        email: formEmail,
        password: formPassword,
      }, { withCredentials: true });
  }

  signOutUser() {
    return this.http.get(this.url + `/api/Users/signOut`, { withCredentials: true }).pipe(
      map(() => {
        this.isLoggedIn = false;
      })
    );
  }

  checkAuthStatus() {
    return this.http.get<any>(this.url + `/api/Users/authStatus`, { withCredentials: true }).pipe<any>(
      map(res => {
        if (res.isLoggedIn) {
          this.isLoggedIn = true;
        }
      })
    );
  }
}
