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

  constructor(public cartService: CartService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.cartService.getCart();
  }

  addToCart(productId: number): void {
    this.cartService.addToCart(productId);
  }

  removeFromCart(productId: number): void {
    this.cartService.removeFromCart(productId);
  }

  purchase(): void {
    this.cartService.purchase().subscribe({
      next: () => {
        this.cartService.getCart();
        this.toastr.success("Purchase successful");
        this.router.navigate([""]);
      },
      error: err => {
        console.log(err.message);
      }
    });
  }

}
