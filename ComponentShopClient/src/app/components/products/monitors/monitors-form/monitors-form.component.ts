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
    if (this.monitorService.currentMonitor.id == 0) {
      this.monitorService.postMonitor();
    }
    else {
      this.monitorService.putMonitor(this.monitorService.currentMonitor.id);
    }
    this.parent.hideForm();
  }

  onFileUpload(event) {
    const file: File = event.target.files[0];
    if (file) {
      this.monitorService.formData.append("imageName", file.name);
      this.monitorService.formData.append("imageFile", file);
    }
  }
}
