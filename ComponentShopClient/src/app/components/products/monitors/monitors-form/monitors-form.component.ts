import { Component } from '@angular/core';
import { MonitorService } from '../../../../shared/monitor.service';
import { MonitorsComponent } from '../monitors.component';

@Component({
  selector: 'app-monitors-form',
  templateUrl: './monitors-form.component.html',
  styleUrl: './monitors-form.component.css'
})
export class MonitorsFormComponent {

  constructor(public monitorService: MonitorService, private parent: MonitorsComponent) { }

  onSubmit() {
    if (this.monitorService.formData.id == 0) {
      this.monitorService.postMonitor();
    }
    else {
      this.monitorService.putMonitor(this.monitorService.formData.id, this.monitorService.formData);
    }
    this.parent.hideForm();
  }
}
