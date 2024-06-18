import { Component } from '@angular/core';
import { MonitorService } from '../../../shared/monitor.service';
import { ActivatedRoute } from '@angular/router';
import { Monitor } from '../../../models/monitor';

@Component({
  selector: 'app-monitor-info',
  templateUrl: './monitor-info.component.html',
  styleUrl: './monitor-info.component.css'
})
export class MonitorInfoComponent {

  constructor(public monitorService: MonitorService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.retrieveMonitorFromRoute();
  }

  retrieveMonitorFromRoute(): void {
    this.route.paramMap.subscribe(params => {
      const id = +params.get('id');
      if (id) {
        this.monitorService.getMonitor(id).subscribe((res: Monitor) => {
          this.monitorService.currentMonitor = res;
        });
      }
    });
  }
}
