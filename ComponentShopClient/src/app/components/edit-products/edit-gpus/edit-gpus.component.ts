import { Component, HostListener } from '@angular/core';
import { GpuService } from '../../../shared/gpu.service';

@Component({
  selector: 'app-edit-gpus',
  templateUrl: './edit-gpus.component.html',
  styleUrl: './edit-gpus.component.css'
})
export class EditGpusComponent {

  isFormVisible: boolean = false;

  constructor(public gpuService: GpuService) { }

  ngOnInit() {
    this.gpuService.getGpus();
  }

  fillForm(id: number) {
    this.showForm();
    this.gpuService.getGpu(id);
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

  @HostListener("window:resize", []) updatePageSize() {
    // lg (for laptops and desktops - screens equal to or greater than 1200px wide)
    // md (for small laptops - screens equal to or greater than 992px wide)
    // sm (for tablets - screens equal to or greater than 768px wide)
    // xs (for phones - screens less than 768px wide)

    if (window.innerHeight >= 1200) {
      this.gpuService.pageSize = 7; // lg
    } else if (window.innerHeight >= 992) {
      this.gpuService.pageSize = 5;//md
    } else if (window.innerHeight >= 768) {
      this.gpuService.pageSize = 4;//sm
    } else if (window.innerHeight < 768) {
      this.gpuService.pageSize = 3;//xs
    }
    this.gpuService.getGpus();
  }

}
