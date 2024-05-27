import { Component } from '@angular/core';
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

}
