import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Gpu } from '../models/gpu';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class GpuService {

  url: string = `${environment.apiBaseUrl}/Gpus`;
  gpuList: Array<Gpu> = [];
  formData: Gpu = new Gpu();
  currentPage: number = 1;
  pageSize: number = 5;
  lastPage: number = 0;
  totalGpus: number = 0;
  searchParam: string = "";

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  resetForm() {
    this.formData = new Gpu();
  }

  getGpus() {
    let uri: string = this.url + `/?currentPage=${this.currentPage}&pageSize=${this.pageSize}`;
    if (this.searchParam != "") {
      uri = uri.concat(`&name=${this.searchParam}`);
    }
    return this.http.get(uri, { observe: 'response', withCredentials: true }).subscribe({
      next: res => {
        this.gpuList = res.body as Array<Gpu>;
        this.totalGpus = parseInt(res.headers.get("X-Total-Count"));
        this.lastPage = Math.floor(this.totalGpus / this.pageSize) + 1;
        if (this.totalGpus % this.pageSize == 0) this.lastPage -= 1;
      },
      error: err => {
        console.log(err);
      }
    });
  }

  getGpu(id: number) {
    return this.http.get(this.url + `/${id}`, { withCredentials: true }).subscribe(
      {
        next: res => {
          this.formData = res as Gpu;
        },
        error: err => {
          console.log(err);
        }
      });
  }

  postGpu() {
    return this.http.post(this.url, this.formData, { withCredentials: true }).subscribe({
      next: res => {
        this.getGpus();
        this.resetForm();
        this.toastr.success("Added");
      },
      error: err => {
        console.log(err);
      }
    });
  }

  deleteGpu(id: number) {
    return this.http.delete(this.url + `/${id}`, { withCredentials: true }).subscribe({
      next: res => {
        if (this.totalGpus % this.pageSize == 1 && this.currentPage == this.lastPage && this.currentPage > 1) {
          this.currentPage -= 1;
        }
        this.getGpus();
        this.toastr.success("Deleted");
      },
      error: err => {
        console.log(err);
      }
    });
  }

  putGpu(id: number, gpu: Gpu) {
    return this.http.put(this.url + `/${id}`, gpu, { withCredentials: true }).subscribe({
      next: res => {
        this.getGpus();
        this.resetForm();
        this.toastr.success("Updated");
      },
      error: err => {
        console.log(err);
      }
    })
  }
}
