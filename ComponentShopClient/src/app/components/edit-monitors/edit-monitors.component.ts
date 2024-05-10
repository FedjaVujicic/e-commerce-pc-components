import { Component } from '@angular/core';
import { MonitorService } from '../../shared/monitor.service';

@Component({
  selector: 'app-edit-monitors',
  templateUrl: './edit-monitors.component.html',
  styleUrl: './edit-monitors.component.css'
})
export class EditMonitorsComponent {

  constructor(public monitorService: MonitorService) { }

  ngOnInit() {
    this.monitorService.getMonitors();
  }

  fillForm(id: number) {
    this.monitorService.getMonitor(id);
  }

  deleteMonitor(id: number) {
    this.monitorService.deleteMonitor(id);
  }
}
