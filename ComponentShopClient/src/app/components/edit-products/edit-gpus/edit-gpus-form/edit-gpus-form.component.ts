import { Component } from '@angular/core';
import { GpuService } from '../../../../shared/gpu.service';
import { EditGpusComponent } from '../edit-gpus.component';

@Component({
  selector: 'app-edit-gpus-form',
  templateUrl: './edit-gpus-form.component.html',
  styleUrl: './edit-gpus-form.component.css'
})
export class EditGpusFormComponent {

  constructor(public gpuService: GpuService, private parent: EditGpusComponent) { }

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
