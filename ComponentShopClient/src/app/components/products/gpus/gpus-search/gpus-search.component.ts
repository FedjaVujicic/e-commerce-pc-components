import { Component } from '@angular/core';
import { GpuService } from '../../../../shared/gpu.service';

@Component({
  selector: 'app-gpus-search',
  templateUrl: './gpus-search.component.html',
  styleUrl: './gpus-search.component.css'
})
export class GpusSearchComponent {

  supportedSlots: Array<string>;
  supportedPorts: Array<string>;
  supportedMemories;

  constructor(public gpuService: GpuService) { }

  ngOnInit() {
    this.gpuService.getSupportedProperties().subscribe({
      next: res => {
        this.supportedSlots = res.slots;
        this.supportedPorts = res.ports;
        this.supportedMemories = res.memories;
      },
      error: err => {
        console.log(err);
      }
    })
  }

  togglePortSelection(port: string, isChecked: boolean) {
    if (isChecked) {
      this.gpuService.selectedPorts[port] = true;
    } else {
      delete this.gpuService.selectedPorts[port];
    }
  }
}
