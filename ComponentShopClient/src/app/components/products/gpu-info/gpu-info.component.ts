import { Component } from '@angular/core';
import { GpuService } from '../../../shared/gpu.service';
import { ActivatedRoute } from '@angular/router';
import { Gpu } from '../../../models/gpu';
import { CommentService } from '../../../shared/comment.service';
import { UserComment } from '../../../models/user-comment';

@Component({
  selector: 'app-gpu-info',
  templateUrl: './gpu-info.component.html',
  styleUrl: './gpu-info.component.css'
})
export class GpuInfoComponent {

  userComments: Array<UserComment> = new Array<UserComment>();

  // Comment that the user is submitting
  commentText: string = "";

  constructor(public gpuService: GpuService, public commentService: CommentService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.retrieveGpuFromRoute();
  }

  retrieveGpuFromRoute(): void {
    this.route.paramMap.subscribe(params => {
      const id = +params.get('id');
      if (id) {
        this.gpuService.getGpu(id).subscribe((res: Gpu) => {
          this.gpuService.currentGpu = res;
        });
        this.commentService.getComments(id).subscribe((res: Array<UserComment>) => {
          this.userComments = res;
        });
      }
    });
  }
}
