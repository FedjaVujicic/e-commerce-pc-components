import { Component } from '@angular/core';
import { CartService } from '../../shared/cart.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {

  errorMessage: string = "";

  constructor(public cartService: CartService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.cartService.getCart();
  }

  addToCart(productId: number) {
    this.cartService.addToCart(productId).subscribe({
      next: () => {
        this.cartService.getCart();
      },
      error: err => {
        console.log(err.error.message);
      }
    });
  }

  removeFromCart(productId: number): void {
    this.cartService.removeFromCart(productId).subscribe({
      next: () => {
        this.cartService.getCart();
      },
      error: err => {
        console.log(err.error.message);
      }
    });
  }

  purchase(): void {
    this.cartService.purchase().subscribe({
      next: () => {
        this.cartService.getCart();
        this.toastr.success("Purchase successful");
        this.router.navigate([""]);
      },
      error: err => {
        this.errorMessage = err.error.message;
      }
    });
  }

}
