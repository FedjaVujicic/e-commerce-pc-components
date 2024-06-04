import { Component, HostListener } from '@angular/core';
import { GpuService } from '../../../shared/gpu.service';
import { UserService } from '../../../shared/user.service';
import { Gpu } from '../../../models/gpu';

@Component({
  selector: 'app-gpus',
  templateUrl: './gpus.component.html',
  styleUrl: './gpus.component.css'
})
export class GpusComponent {

  isFormVisible: boolean = false;

  constructor(public gpuService: GpuService, public userService: UserService) { }

  ngOnInit() {
    this.gpuService.getGpus();
  }

  fillForm(id: number) {
    if (!this.userService.isAdminLoggedIn) {
      return;
    }
    this.showForm();
    this.gpuService.getGpu(id).subscribe(res => {
      this.gpuService.currentGpu = res as Gpu;
      this.gpuService.getQuantity(id).subscribe(res => {
        this.gpuService.currentGpu.quantity = res as number;
      });
    }
    );
  }

  showForm() {
    this.gpuService.resetForm();
    this.isFormVisible = true;
  }

  hideForm() {
    this.gpuService.resetForm();
    this.isFormVisible = false;
  }

  pageNext() {
    this.gpuService.currentPage = this.gpuService.currentPage + 1;
    this.gpuService.getGpus();
  }

  pagePrev() {
    this.gpuService.currentPage = this.gpuService.currentPage - 1;
    this.gpuService.getGpus();
  }

  setPageSize(pageSize: number) {
    this.gpuService.pageSize = pageSize;
    this.gpuService.getGpus();
  }

  sortByPriceAscending() {
    this.gpuService.gpuList.sort((m1, m2) => {
      if (m1.price > m2.price) {
        return 1;
      }
      if (m1.price < m2.price) {
        return -1;
      }
      return 0;
    })
  }

  sortByPriceDescending() {
    this.gpuService.gpuList.sort((m1, m2) => {
      if (m1.price > m2.price) {
        return -1;
      }
      if (m1.price < m2.price) {
        return 1;
      }
      return 0;
    })
  }
}
