import { Component, HostListener } from '@angular/core';
import { GpuService } from '../../../shared/gpu.service';
import { UserService } from '../../../shared/user.service';
import { Gpu } from '../../../models/gpu';
import { Router } from '@angular/router';
import { CartService } from '../../../shared/cart.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-gpus',
  templateUrl: './gpus.component.html',
  styleUrl: './gpus.component.css'
})
export class GpusComponent {

  isFormVisible: boolean = false;

  constructor(public gpuService: GpuService, public userService: UserService, private router: Router,
    public cartService: CartService, private toastr: ToastrService) { }

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

  goToGpu(id: number): void {
    this.gpuService.getGpu(id).subscribe((res: Gpu) => {
      this.gpuService.currentGpu = res;
      this.router.navigate(["../gpu-info", id]);
    });
  }

  addToCart(productId: number) {
    this.cartService.addToCart(productId).subscribe({
      next: () => {
        this.toastr.success("Added");
      },
      error: err => {
        console.log(err.error.message);
      }
    });
  }
}
