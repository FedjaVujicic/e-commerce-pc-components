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
    if (this.gpuService.formData.ports.length == 0) {
      this.noPorts = true;
      return false;
    }
    return true;
  }

  onSubmit(): void {
    this.validateForm();
    if (this.gpuService.formData.id == 0) {
      this.gpuService.postGpu();
    }
    else {
      this.gpuService.putGpu(this.gpuService.formData.id, this.gpuService.formData);
    }
    this.parent.hideForm();
  }

  addPort(): void {
    this.gpuService.formData.ports.push(this.currentPort);
    this.currentPort = "";
  }

  removePort(port: string): void {
    const index = this.gpuService.formData.ports.indexOf(port);
    if (index > -1) {
      this.gpuService.formData.ports.splice(index, 1);
    }
  }
}
