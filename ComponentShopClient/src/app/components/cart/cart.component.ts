import { Component } from '@angular/core';
import { CartService } from '../../shared/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {

  constructor(public cartService: CartService) { }

  ngOnInit(): void {
    this.cartService.getCart();
  }

}
