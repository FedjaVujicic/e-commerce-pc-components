import { Component } from '@angular/core';
import { GpuService } from '../../../shared/gpu.service';
import { ActivatedRoute } from '@angular/router';
import { Gpu } from '../../../models/gpu';

@Component({
  selector: 'app-gpu-info',
  templateUrl: './gpu-info.component.html',
  styleUrl: './gpu-info.component.css'
})
export class GpuInfoComponent {

  constructor(public gpuService: GpuService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.retrieveGpuFromRoute();
  }

  retrieveGpuFromRoute(): void {
    this.route.paramMap.subscribe(params => {
      const id = +params.get('id');
      if (id) {
        this.gpuService.getGpu(id).subscribe((res: Gpu) => {
          this.gpuService.currentGpu = res;
        });
      }
    });
  }
}
