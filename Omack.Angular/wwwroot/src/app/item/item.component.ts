import { Component } from '@angular/core';
import { Item } from "../../Models/Item";
import { ItemService } from "../../Services/ItemService";

@Component({
    selector: 'list-item',
    //templateUrl: '/partial/itemListComponent'
    templateUrl: 'item.component.html'
})

export class ItemComponent {
    constructor(private itemService: ItemService) { }

    item: Item = null;
    itemId: number = 46;

    hasItems: boolean = false;

    items: Item[] = [];
    groupId: number = 31;

    getItemById(): void {
        this.item = null;
        this.itemService.getItemsById(this.itemId).subscribe((result: Item) => this.item = result);
    }

    getItemsByGroupId(): void {
        this.items = null;
        this.itemService.getItems(this.groupId).subscribe(
            (data: Item[]) => {
                console.log(data);
                this.items = data;
                this.hasItems = true;
            },
            err => this.hasItems = false,
            () => console.log("Success")
        );
    }
}
