import { Component } from '@angular/core';
import { GpuService } from '../../../shared/gpu.service';
import { ActivatedRoute } from '@angular/router';
import { Gpu } from '../../../models/gpu';
import { CommentService } from '../../../shared/comment.service';
import { UserComment } from '../../../models/user-comment';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-gpu-info',
  templateUrl: './gpu-info.component.html',
  styleUrl: './gpu-info.component.css'
})
export class GpuInfoComponent {

  userComments: Array<UserComment> = new Array<UserComment>();
  productId: number;

  // Comment that the user is submitting
  commentText: string = "";

  errorMessage: string = "";

  constructor(public gpuService: GpuService, public commentService: CommentService, private route: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.retrieveGpuFromRoute();
  }

  retrieveGpuFromRoute(): void {
    this.route.paramMap.subscribe(params => {
      this.productId = +params.get('id');
      if (this.productId) {
        this.gpuService.getGpu(this.productId).subscribe((res: Gpu) => {
          this.gpuService.currentGpu = res;
        });
        this.commentService.getComments(this.productId).subscribe((res: Array<UserComment>) => {
          this.userComments = res;
        });
      }
    });
  }

  postComment(): void {
    this.commentService.postComment(this.productId, this.commentText).subscribe({
      next: () => {
        this.toastr.success("Success");
        this.commentService.getComments(this.productId).subscribe((res: Array<UserComment>) => {
          this.userComments = res;
        });
      },
      error: err => {
        this.errorMessage = err.error.message;
      }
    });
  }
}
