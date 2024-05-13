import { Component } from '@angular/core';
import { MonitorService } from '../../shared/monitor.service';

@Component({
  selector: 'app-edit-monitors',
  templateUrl: './edit-monitors.component.html',
  styleUrl: './edit-monitors.component.css'
})
export class EditMonitorsComponent {

  isFormVisible: boolean = false;

  constructor(public monitorService: MonitorService) { }

  ngOnInit() {
    this.monitorService.getMonitors();
  }

  fillForm(id: number) {
    this.showForm();
    this.monitorService.getMonitor(id);
  }

  deleteMonitor(id: number) {
    this.monitorService.deleteMonitor(id);
  }

  showForm() {
    this.monitorService.resetForm();
    this.isFormVisible = true;
  }

  hideForm() {
    this.monitorService.resetForm();
    this.isFormVisible = false;
  }
}
