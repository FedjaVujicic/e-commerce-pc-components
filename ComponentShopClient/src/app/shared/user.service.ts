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

  constructor(private http: HttpClient, private router: Router) { }

  url: string = `${environment.apiBaseUrl}/Users`;
  currentUser: User;
  loginFailed: boolean = false;

  loginUser(formUsername: string, formPassword: string) {
    return this.http.post<{ loginSuccessful: boolean, currentUser: User }>(this.url + `/login/?username=${formUsername}&password=${formPassword}`, {}).pipe(
      tap(res => {
        if (res.loginSuccessful) {
          this.currentUser = res.currentUser as User;
        }
        this.loginFailed = !res.loginSuccessful;
      })
    );
  }

  registerUser(formUsername: string, formPassword: string) {
    return this.http.post<{ userExists: boolean }>(this.url + `/register/?username=${formUsername}&password=${formPassword}`, {});
  }
}
