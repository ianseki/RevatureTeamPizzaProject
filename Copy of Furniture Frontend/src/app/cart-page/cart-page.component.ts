import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CartService } from '../services/cart.service';
import { FurnitureService } from '../services/furniture/furniture.service';
import { Cart } from '../shared/models/Cart';
import { CartItem } from '../shared/models/cartItem';
import { Furnitures } from '../shared/models/furniture';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrls: ['./cart-page.component.css'],
})

export class CartPageComponent implements OnInit 
{
  cart!: Cart;
  genericArr!: number[];
  arrayCartItems!: Furnitures[];

  count!: number;

  public cartItemList : any =[];
  public productList = new BehaviorSubject<any>([]);

  totalCount: number = 0;
  totalCost: number = 0;

  constructor(private cartService: CartService) {
    this.setCart();
  }

  ngOnInit(): void 
  {
    this.genericArr = new Array<number>(5);
    this.arrayCartItems = new Array<Furnitures>;
  }

  setCart() {
    this.cart = this.cartService.getCart();
  }

  removeFromCart(cartItem: CartItem) {
    this.cartService.removeFromCart(cartItem.furniture.id);
    this.setCart();
  }

  changeQuantity(cartItem: CartItem, quantityInString: string) {
    const quantity = parseInt(quantityInString);
    this.cartService.changeQuantity(cartItem.furniture.id, quantity);
    this.setCart();
  }

  testworks()
  {
    console.log(this.count);
  }

  addtoCart(product : any, count: number){
    for (let i = 1; i <= count; i++)
    {
      this.cartItemList.push(product);
    }
    // this.productList.next(this.cartItemList);
    this.totalCost = this.getTotalPrice();
    this.totalCount = count;
    console.log(this.cartItemList);
    console.log(this.totalCost);
  }

  getTotalPrice() : number{
    let total = 0;
    this.cartItemList.map((a: any)=>{
      total += a.price;
    })
    return total;
  }

  
}
