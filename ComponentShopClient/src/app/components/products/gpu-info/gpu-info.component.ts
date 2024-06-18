import { Component } from '@angular/core';
import { GpuService } from '../../../shared/gpu.service';

@Component({
  selector: 'app-gpu-info',
  templateUrl: './gpu-info.component.html',
  styleUrl: './gpu-info.component.css'
})
export class GpuInfoComponent {

  constructor(public gpuService: GpuService) { }
}
