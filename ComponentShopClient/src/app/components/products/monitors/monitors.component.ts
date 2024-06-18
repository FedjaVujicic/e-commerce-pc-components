import { Component, HostListener } from '@angular/core';
import { MonitorService } from '../../../shared/monitor.service';
import { UserService } from '../../../shared/user.service';
import { Monitor } from '../../../models/monitor';
import { Router } from '@angular/router';

@Component({
  selector: 'app-monitors',
  templateUrl: './monitors.component.html',
  styleUrl: './monitors.component.css'
})
export class MonitorsComponent {

  isFormVisible: boolean = false;

  constructor(public monitorService: MonitorService, public userService: UserService, private router: Router) { }

  ngOnInit() {
    this.monitorService.getMonitors();
  }

  fillForm(id: number) {
    if (!this.userService.isAdminLoggedIn) {
      return;
    }
    this.showForm();
    this.monitorService.getMonitor(id).subscribe(res => {
      this.monitorService.currentMonitor = res as Monitor;
      this.monitorService.getQuantity(id).subscribe(res => {
        this.monitorService.currentMonitor.quantity = res as number;
      });
    }
    );
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

  setPageSize(pageSize: number) {
    this.monitorService.pageSize = pageSize;
    this.monitorService.getMonitors();
  }

  sortByPriceAscending() {
    this.monitorService.monitorList.sort((m1, m2) => {
      if (m1.price > m2.price) {
        return 1;
      }
      if (m1.price < m2.price) {
        return -1;
      }
      return 0;
    })
  }

  sortByPriceDescending() {
    this.monitorService.monitorList.sort((m1, m2) => {
      if (m1.price > m2.price) {
        return -1;
      }
      if (m1.price < m2.price) {
        return 1;
      }
      return 0;
    })
  }

  goToMonitor(id: number): void {
    this.monitorService.getMonitor(id).subscribe((res: Monitor) => {
      this.monitorService.currentMonitor = res;
      this.router.navigate(["../monitor-info", id]);
    });
  }
}

