import { Component } from '@angular/core';
import { MonitorService } from '../../../../shared/monitor.service';

@Component({
  selector: 'app-monitors-search',
  templateUrl: './monitors-search.component.html',
  styleUrl: './monitors-search.component.css'
})
export class MonitorsSearchComponent {

  supportedResolutions: Array<string>;
  supportedRefreshRates: Array<string>;

  constructor(public monitorService: MonitorService) { }

  ngOnInit() {
    this.monitorService.getSupportedProperties().subscribe({
      next: res => {
        this.supportedResolutions = res.resolutions;
        this.supportedRefreshRates = res.refreshRates;
      },
      error: err => {
        console.log(err);
      }
    });
  }
}
