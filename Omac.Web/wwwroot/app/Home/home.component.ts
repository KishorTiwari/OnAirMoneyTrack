import { Component, OnInit } from '@angular/core';
import { Item } from '../Models/Item';
import { ItemService } from '../Services/ItemService'

@Component({
    selector: 'home-content',
    templateUrl: '/partial/homeComponent'
})

export class HomeComponent implements OnInit {
    items: Item[] = [];

    constructor(private _itemService: ItemService) { }

    ngOnInit(): void {

    }
}