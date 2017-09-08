import { Component, OnInit } from '@angular/core';
import {ItemService} from '../../Services/ItemService';
import { Item } from "../../Models/Item";
import { Observable } from "rxjs/Observable";

@Component({
    selector: 'test',
    templateUrl: 'test.component.html',
    styleUrls: ['test.component.css']
})

export class TestComponent implements OnInit {

    //dependencty injection via constructor
    constructor(private itemService: ItemService) {
        //itemService.getItems(this.groupId).subscribe((result: Item[]) => this.items = result);
    }

    //runs after all the dependencies, functions and properties of component are initialized.
    ngOnInit(): void {   
        this.getItemsByGroupId();
    }

    //properties
    item: Item;
    itemId: number = 46;
    items: Item[];
    groupId: number = 31;


    //methods
    getItemById(): void {
        this.item = null;
        this.itemService.getItemsById(this.itemId).subscribe((result: Item) => this.item = result);
    }

    getItemsByGroupId(): void {
        this.itemService.getItems(this.groupId).subscribe(
            (data: Item[]) => {
                this.items = data;  //set property value
            },
            err => { this.items = null;},   //catch error
            () => console.log("Success")   //function complete
        );
    }

    trackByItemId(index: number, item: Item): number {
        return item.id;
    }

    getTotalCount(): number {
        return this.items.length;
    }
    getTotalExpensiveCount(): number {
        return this.items.filter(x => x.price >= 100).length;
    }
    getTotalCheapCount(): number {
        return this.items.filter(x => x.price < 100).length;
    }
}
