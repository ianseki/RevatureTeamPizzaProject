import { Component, OnInit } from '@angular/core';
import { FurnitureService } from '../services/furniture/furniture.service';
import { Furnitures } from '../shared/models/furniture';
import { ActivatedRoute } from '@angular/router';
import { identifierName } from '@angular/compiler';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  furnitures: Furnitures[] = [];

  constructor(private fs: FurnitureService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      if (params['searchItem'])
        this.furnitures = this.fs
          .getAll()
          .filter((furniture) =>
            furniture.name
              .toLocaleLowerCase()
              .includes(params['searchItem'].toLocaleLowerCase())
          );
      else this.furnitures = this.fs.getAll();
    });
  }
}
