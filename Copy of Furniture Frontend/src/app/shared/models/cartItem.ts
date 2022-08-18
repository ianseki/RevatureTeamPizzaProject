import { Furnitures } from './furniture';

export class CartItem {
  constructor(furniture: Furnitures) {
    this.furniture = furniture;
  }

  furniture: Furnitures;
  quantity: number = 1;
  get price(): Number {
    return this.furniture.price * this.quantity;
  }
}
