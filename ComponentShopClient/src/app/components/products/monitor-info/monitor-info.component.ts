import { Component } from '@angular/core';
import { MonitorService } from '../../../shared/monitor.service';

@Component({
  selector: 'app-monitor-info',
  templateUrl: './monitor-info.component.html',
  styleUrl: './monitor-info.component.css'
})
export class MonitorInfoComponent {

  constructor(public monitorService: MonitorService) { }

}
