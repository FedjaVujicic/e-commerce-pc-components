import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { RegistrationDto } from '../models/registration-dto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private router: Router) { }

  url: string = `https://localhost:7142`;
  isLoggedIn: boolean = false;

  loginUser(formEmail: string, formPassword: string) {
    return this.http.post(this.url + `/login/?useCookies=true`,
      {
        email: formEmail,
        password: formPassword,
      }, { withCredentials: true });
  }

  registerUser(userDto: RegistrationDto) {
    return this.http.post(this.url + `/api/Users/register`, userDto, { withCredentials: true });
  }

  signOutUser() {
    return this.http.get(this.url + `/api/Users/signOut`, { withCredentials: true }).pipe(
      map(() => {
        localStorage.removeItem("currentUser");
        this.isLoggedIn = false;
      })
    );
  }

  getUserInfo() {
    return this.http.get<any>(this.url + `/api/Users/userInfo`, { observe: 'response', withCredentials: true }).pipe<any>(
      map(res => {
        if (res.status == 204) {
          return;
        }
        localStorage.setItem("currentUser", JSON.stringify({
          username: res.body.username,
          role: res.body.role,
          firstName: res.body.firstName,
          lastName: res.body.lastName,
          birthday: res.body.birthday,
          credits: res.body.credits
        }
        ));
        this.isLoggedIn = true;
      })
    );
  }

  isAdminLoggedIn() {
    if (!this.isLoggedIn) {
      return false;
    }
    let role = JSON.parse(localStorage.getItem("currentUser")).role;
    if (role != "Admin") {
      return false;
    }
    return true;
  }
}
