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

  searchName: string = "";
  priceLow: number = null;
  priceHigh: number = null;
  availableOnly: boolean = false;
  slot: string = "";
  memory = null;
  selectedPorts: { [key: string]: boolean } = {};

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  resetForm() {
    this.formData = new Gpu();
  }

  getGpus() {
    let uri: string = this.buildGetUri();

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
    if (this.slot != "") {
      uri = uri.concat(`&slot=${this.slot}`);
    }
    if (this.memory != null && this.memory != "") {
      uri = uri.concat(`&memory=${this.memory}`);
    }
    for (const port in this.selectedPorts) {
      uri = uri.concat(`&ports=${port}`);
    }
    return uri;
  }
}
