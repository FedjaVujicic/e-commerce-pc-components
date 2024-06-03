import { Component } from '@angular/core';
import { GpuService } from '../../../../shared/gpu.service';
import { GpusComponent } from '../gpus.component';

@Component({
  selector: 'app-gpus-form',
  templateUrl: './gpus-form.component.html',
  styleUrl: './gpus-form.component.css'
})
export class GpusFormComponent {

  constructor(public gpuService: GpuService, private parent: GpusComponent) { }

  currentPort: string;
  noPorts: boolean;

  validateForm(): boolean {
    if (this.gpuService.currentGpu.ports.length == 0) {
      this.noPorts = true;
      return false;
    }
    return true;
  }

  onSubmit(): void {
    this.validateForm();
    if (this.gpuService.currentGpu.id == 0) {
      this.gpuService.postGpu();
    }
    else {
      this.gpuService.putGpu(this.gpuService.currentGpu.id);
    }
    this.parent.hideForm();
  }

  addPort(): void {
    this.gpuService.currentGpu.ports.push(this.currentPort);
    this.currentPort = "";
  }

  removePort(port: string): void {
    const index = this.gpuService.currentGpu.ports.indexOf(port);
    if (index > -1) {
      this.gpuService.currentGpu.ports.splice(index, 1);
    }
  }
  onFileUpload(event) {
    const file: File = event.target.files[0];
    if (file) {
      this.gpuService.formData.append("imageName", file.name);
      this.gpuService.formData.append("imageFile", file);
    }
  }
}
