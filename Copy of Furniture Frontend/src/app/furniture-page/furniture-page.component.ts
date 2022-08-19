import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CartService } from '../services/cart.service';
import { FurnitureService } from '../services/furniture/furniture.service';
import { Furnitures } from '../shared/models/furniture';

@Component({
  selector: 'app-furniture-page',
  templateUrl: './furniture-page.component.html',
  styleUrls: ['./furniture-page.component.css'],
})
export class FurniturePageComponent implements OnInit {
  furniture!: Furnitures;

  constructor(
    private activatedRoute: ActivatedRoute,
    private furnitureService: FurnitureService,
    private cartService: CartService,
    private router: Router) {
    activatedRoute.params.subscribe((params) => {
      if (params['id'])
        this.furniture = furnitureService.getFurnitureById(params['id']);
    });
  }

  ngOnInit(): void {}

  addToCart() {
    this.cartService.addToCart(this.furniture);
    this.router.navigateByUrl('/cart-page');
  }
}
