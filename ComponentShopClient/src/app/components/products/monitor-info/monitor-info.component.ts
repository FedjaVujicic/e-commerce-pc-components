import { Component } from '@angular/core';
import { MonitorService } from '../../../shared/monitor.service';
import { ActivatedRoute } from '@angular/router';
import { Monitor } from '../../../models/monitor';
import { CommentService } from '../../../shared/comment.service';
import { UserComment } from '../../../models/user-comment';

@Component({
  selector: 'app-monitor-info',
  templateUrl: './monitor-info.component.html',
  styleUrl: './monitor-info.component.css'
})
export class MonitorInfoComponent {

  userComments: Array<UserComment> = new Array<UserComment>();

  // Comment that the user is submitting
  commentText: string = "";

  constructor(public monitorService: MonitorService, public commentService: CommentService, private route: ActivatedRoute) { }

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
        this.commentService.getComments(id).subscribe((res: Array<UserComment>) => {
          this.userComments = res;
        });
      }
    });
  }
}
