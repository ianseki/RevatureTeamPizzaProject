import { Injectable } from '@angular/core';
import { Cart } from '../shared/models/Cart';
import { Furnitures } from '../shared/models/furniture';
import { CartItem } from '../shared/models/cartItem';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cart: Cart = new Cart();

  addToCart(furniture: Furnitures): void {
    let cartItem = this.cart.items.find(
      (item) => item.furniture.id === furniture.id
    );
    if (cartItem) {
      this.changeQuantity(furniture.id, cartItem.quantity + 1);
      return;
    }
    this.cart.items.push(new CartItem(furniture));
  }

  removeFromCart(furnitureId: number): void {
    this.cart.items = this.cart.items.filter(
      (item) => item.furniture.id != furnitureId
    );
  }

  changeQuantity(quantity: number, furnitureId: number) {
    let cartItem = this.cart.items.find(
      (item) => item.furniture.id === furnitureId
    );
    if (!cartItem) return;
    cartItem.quantity = quantity;
  }

  getCart(): Cart {
    return this.cart;
  }
}
