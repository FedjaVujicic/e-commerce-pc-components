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
    return this.http.get(this.url).subscribe(monitor => {
      this.monitorList = monitor as Array<Monitor>;
    });
  }

  getMonitor(id: number) {
    return this.http.get(this.url + `/${id}`).subscribe(monitor => {
      this.formData = monitor as Monitor;
    });
  }

  postMonitor() {
    return this.http.post(this.url, this.formData).subscribe(monitor => {
      this.monitorList = monitor as Array<Monitor>;
      this.resetForm();
      this.toastr.success("Added");
    });
  }

  deleteMonitor(id: number) {
    return this.http.delete(this.url + `/${id}`).subscribe(monitor => {
      this.monitorList = monitor as Array<Monitor>;
      this.toastr.success("Deleted");
    });
  }

  putMonitor(id: number, monitor: Monitor) {
    return this.http.put(this.url + `/${id}`, monitor).subscribe(monitor => {
      this.monitorList = monitor as Array<Monitor>;
      this.resetForm();
      this.toastr.success("Updated");
    })
  }
}
