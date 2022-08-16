import { Injectable } from '@angular/core';
import { Furnitures } from 'src/app/shared/models/furniture';

@Injectable({
  providedIn: 'root',
})
export class FurnitureService {
  constructor() {}

  getFurnitureById(id: number): Furnitures {
    return this.getAll().find((furniture) => furniture.id == id)!;
  }

  getAll(): Furnitures[] {
    return [
      {
        id: 1,
        name: 'Chair',
        price: 80,
        timeFrame: '2-3 months',
        numberofEmployees: 3,
        star: 4.5,
        imageUrl: '/assets/chair.jpg',
        tag: null,
      },
      {
        id: 2,
        name: 'Table',
        price: 80,
        timeFrame: '3-6 months',
        numberofEmployees: 6,
        imageUrl: '/assets/table.jpg',
        star: 3,
        tag: null,
      },
      {
        id: 3,
        name: 'Desk',
        price: 80,
        timeFrame: '6-9 months',
        numberofEmployees: 8,
        imageUrl: '/assets/desk.jpg',
        star: 5,
        tag: null,
      },
    ];
  }
}
