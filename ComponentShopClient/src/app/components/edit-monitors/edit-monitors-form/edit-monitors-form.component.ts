import { Component } from '@angular/core';
import { MonitorService } from '../../../shared/monitor.service';
import { Monitor } from '../../../models/monitor';

@Component({
  selector: 'app-edit-monitors-form',
  templateUrl: './edit-monitors-form.component.html',
  styleUrl: './edit-monitors-form.component.css'
})
export class EditMonitorsFormComponent {

  constructor(public monitorService: MonitorService) { }

  onSubmit() {
    if (this.monitorService.formData.id == 0) {
      this.monitorService.postMonitor();
    }
    else {
      this.monitorService.putMonitor(this.monitorService.formData.id, this.monitorService.formData);
    }
  }
}
