import { Component } from '@angular/core';
import { MonitorService } from '../../../shared/monitor.service';
import { ActivatedRoute } from '@angular/router';
import { Monitor } from '../../../models/monitor';
import { CommentService } from '../../../shared/comment.service';
import { UserComment } from '../../../models/user-comment';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-monitor-info',
  templateUrl: './monitor-info.component.html',
  styleUrl: './monitor-info.component.css'
})
export class MonitorInfoComponent {

  userComments: Array<UserComment> = new Array<UserComment>();
  productId: number;

  // Comment that the user is submitting
  commentText: string = "";

  constructor(public monitorService: MonitorService, public commentService: CommentService, private route: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.retrieveMonitorFromRoute();
  }

  retrieveMonitorFromRoute(): void {
    this.route.paramMap.subscribe(params => {
      this.productId = +params.get('id');
      if (this.productId) {
        this.monitorService.getMonitor(this.productId).subscribe((res: Monitor) => {
          this.monitorService.currentMonitor = res;
        });
        this.commentService.getComments(this.productId).subscribe((res: Array<UserComment>) => {
          this.userComments = res;
        });
      }
    });
  }

  postComment(): void {
    this.commentService.postComment(this.productId, this.commentText).subscribe(() => {
      this.toastr.success("Success");
      this.commentService.getComments(this.productId);
    });
  }
}
