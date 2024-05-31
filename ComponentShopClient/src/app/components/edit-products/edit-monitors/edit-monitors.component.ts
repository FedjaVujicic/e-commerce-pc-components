import { Component, HostListener } from '@angular/core';
import { MonitorService } from '../../../shared/monitor.service';

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

  showForm() {
    this.monitorService.resetForm();
    this.isFormVisible = true;
  }

  hideForm() {
    this.monitorService.resetForm();
    this.isFormVisible = false;
  }

  pageNext() {
    this.monitorService.currentPage = this.monitorService.currentPage + 1;
    this.monitorService.getMonitors();
  }

  pagePrev() {
    this.monitorService.currentPage = this.monitorService.currentPage - 1;
    this.monitorService.getMonitors();
  }

  @HostListener("window:resize", []) updatePageSize() {
    // lg (for laptops and desktops - screens equal to or greater than 1200px wide)
    // md (for small laptops - screens equal to or greater than 992px wide)
    // sm (for tablets - screens equal to or greater than 768px wide)
    // xs (for phones - screens less than 768px wide)

    if (window.innerHeight >= 1200) {
      this.monitorService.pageSize = 7; // lg
    } else if (window.innerHeight >= 992) {
      this.monitorService.pageSize = 5;//md
    } else if (window.innerHeight >= 768) {
      this.monitorService.pageSize = 4;//sm
    } else if (window.innerHeight < 768) {
      this.monitorService.pageSize = 3;//xs
    }
    this.monitorService.getMonitors();
  }

}
