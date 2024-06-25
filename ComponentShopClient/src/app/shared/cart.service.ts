import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  url: string = `${environment.apiBaseUrl}/Cart`;

  constructor(private http: HttpClient) { }

  getCart(): Observable<Object> {
    return this.http.get(this.url, { withCredentials: true });
  }
}
