import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Gpu } from '../models/gpu';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class GpuService {

  url: string = `${environment.apiBaseUrl}/Products`;

  gpuList: Array<Gpu> = [];
  currentGpu: Gpu = new Gpu();
  formData: FormData = new FormData();
  imageName: string;
  imageFile: File;

  currentPage: number = 1;
  pageSize: number = 5;
  lastPage: number = 0;
  totalGpus: number = 0;

  searchName: string = "";
  priceLow: number = null;
  priceHigh: number = null;
  availableOnly: boolean = false;
  sort: string = "";
  slot: string = "";
  memory = null;
  selectedPorts: { [key: string]: boolean } = {};

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  resetForm() {
    this.currentGpu = new Gpu();
    this.formData = new FormData();
  }

  getImageSrc(imageFile: any): string {
    if (imageFile === null || imageFile === undefined) {
      return "favicon.ico"
    }
    return 'data:' + imageFile.contentType + ';base64,' + imageFile.fileContents;
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
    return this.http.get(this.url + `/${id}`, { withCredentials: true });
  }

  getQuantity(id: number) {
    return this.http.get(this.url + `/${id}/quantity`, { withCredentials: true });
  }

  postGpu() {
    this.createFormData();
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

  putGpu(id: number) {
    this.createFormData();
    return this.http.put(this.url + `/${id}`, this.formData, { withCredentials: true }).subscribe({
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
    return this.http.get<any>(this.url + `/supportedProperties/?category=gpu`, { withCredentials: true });
  }

  createFormData() {
    this.formData.append("name", this.currentGpu.name);
    this.formData.append("price", this.currentGpu.price.toString());
    this.formData.append("quantity", this.currentGpu.quantity.toString());
    this.formData.append("slot", this.currentGpu.slot);
    this.formData.append("memory", this.currentGpu.memory.toString());
    this.currentGpu.ports.forEach(port => {
      this.formData.append("ports", port.toString());
    });
    this.formData.append("category", "gpu");
    this.formData.append("size", "0");
    this.formData.append("width", "0");
    this.formData.append("height", "0");
    this.formData.append("refreshRate", "0");
  }

  buildGetUri(): string {
    let uri: string = this.url + `/?currentPage=${this.currentPage}&pageSize=${this.pageSize}&category=gpu`;
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
    if (this.sort != "") {
      uri = uri.concat(`&sort=${this.sort}`);
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
