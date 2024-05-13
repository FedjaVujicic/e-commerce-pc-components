import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Monitor } from '../models/monitor';
import { Observable } from 'rxjs';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class MonitorService {

  url: string = `${environment.apiBaseUrl}/Monitors`;
  monitorList: Array<Monitor> = [];
  formData: Monitor = new Monitor();

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  resetForm() {
    this.formData = new Monitor();
  }

  getMonitors() {
    return this.http.get(this.url).subscribe({
      next: res => {
        this.monitorList = res as Array<Monitor>;
      },
      error: err => {
        console.log(err);
      }
    });
  }

  getMonitor(id: number) {
    return this.http.get(this.url + `/${id}`).subscribe(
      {
        next: res => {
          this.formData = res as Monitor;
        },
        error: err => {
          console.log(err);
        }
      });
  }

  postMonitor() {
    return this.http.post(this.url, this.formData).subscribe({
      next: res => {
        this.monitorList = res as Array<Monitor>;
        this.resetForm();
        this.toastr.success("Added");
      },
      error: err => {
        console.log(err);
      }
    });
  }

  deleteMonitor(id: number) {
    return this.http.delete(this.url + `/${id}`).subscribe({
      next: res => {
        this.monitorList = res as Array<Monitor>;
        this.toastr.success("Deleted");
      },
      error: err => {
        console.log(err);
      }
    });
  }

  putMonitor(id: number, monitor: Monitor) {
    return this.http.put(this.url + `/${id}`, monitor).subscribe({
      next: res => {
        this.monitorList = res as Array<Monitor>;
        this.resetForm();
        this.toastr.success("Updated");
      },
      error: err => {
        console.log(err);
      }
    })
  }
}
