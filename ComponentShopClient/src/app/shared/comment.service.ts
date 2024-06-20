import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  url: string = `${environment.apiBaseUrl}/Comments`;

  constructor(private http: HttpClient) { }

  getComments(productId: number): Observable<Object> {
    return this.http.get(this.url + `?productId=${productId}`, { withCredentials: true });
  }

  postComment(productId: number, text: string): Observable<Object> {
    return this.http.post(this.url + `?productId=${productId}`, { productId: productId, text: text }, { withCredentials: true });
  }
}
