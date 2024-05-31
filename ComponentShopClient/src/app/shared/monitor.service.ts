import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Monitor } from '../models/monitor';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class MonitorService {

  url: string = `${environment.apiBaseUrl}/Monitors`;

  monitorList: Array<Monitor> = [];
  formData: Monitor = new Monitor();

  currentPage: number = 1;
  pageSize: number = 5;
  lastPage: number = 0;
  totalMonitors: number = 0;

  searchName: string = "";
  priceLow: number = null;
  priceHigh: number = null;
  availableOnly: boolean = false;
  sizeLow: number = null;
  sizeHigh: number = null;
  resolution: string = null;
  refreshRate = null;

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  resetForm() {
    this.formData = new Monitor();
  }

  getMonitors() {
    let uri: string = this.buildGetUri();

    return this.http.get(uri, { observe: 'response', withCredentials: true }).subscribe({
      next: res => {
        this.monitorList = res.body as Array<Monitor>;
        this.totalMonitors = parseInt(res.headers.get("X-Total-Count"));
        this.lastPage = Math.floor(this.totalMonitors / this.pageSize) + 1;
        if (this.totalMonitors % this.pageSize == 0) this.lastPage -= 1;
      },
      error: err => {
        console.log(err);
      }
    });
  }

  getMonitor(id: number) {
    return this.http.get(this.url + `/${id}`, { withCredentials: true }).subscribe(
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
    return this.http.post(this.url, this.formData, { withCredentials: true }).subscribe({
      next: res => {
        this.getMonitors();
        this.resetForm();
        this.toastr.success("Added");
      },
      error: err => {
        console.log(err);
      }
    });
  }

  deleteMonitor(id: number) {
    return this.http.delete(this.url + `/${id}`, { withCredentials: true }).subscribe({
      next: res => {
        if (this.totalMonitors % this.pageSize == 1 && this.currentPage == this.lastPage && this.currentPage > 1) {
          this.currentPage -= 1;
        }
        this.getMonitors();
        this.toastr.success("Deleted");
      },
      error: err => {
        console.log(err);
      }
    });
  }

  putMonitor(id: number, monitor: Monitor) {
    return this.http.put(this.url + `/${id}`, monitor, { withCredentials: true }).subscribe({
      next: res => {
        this.getMonitors();
        this.resetForm();
        this.toastr.success("Updated");
      },
      error: err => {
        console.log(err);
      }
    })
  }

  getSupportedProperties() {
    return this.http.get<any>(this.url + `/supportedProperties`, { withCredentials: true });
  }

  buildGetUri(): string {
    let uri: string = this.url + `/?currentPage=${this.currentPage}&pageSize=${this.pageSize}`;
    if (this.searchName != "") {
      uri = uri.concat(`&name=${this.searchName}`);
    }
    if (this.priceLow != null) {
      uri = uri.concat(`&priceLow=${this.priceLow}`);
    }
    if (this.priceHigh != null) {
      uri = uri.concat(`&priceHigh=${this.priceHigh}`);
    }
    if (this.availableOnly) {
      uri = uri.concat(`&availableOnly=${this.availableOnly}`);
    }
    if (this.sizeLow != null) {
      uri = uri.concat(`&sizeLow=${this.sizeLow}`);
    }
    if (this.sizeHigh != null) {
      uri = uri.concat(`&sizeHigh=${this.sizeHigh}`);
    }
    if (this.resolution != null && this.resolution != "") {
      uri = uri.concat(`&resolution=${this.resolution}`);
    }
    if (this.refreshRate != null && this.refreshRate != "") {
      uri = uri.concat(`&refreshRate=${this.refreshRate}`);
    }
    return uri;
  }
}
