"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var ItemService_1 = require("../../Services/ItemService");
var ItemComponent = (function () {
    function ItemComponent(itemService) {
        this.itemService = itemService;
        this.item = null;
        this.itemId = 46;
        this.hasItems = false;
        this.items = [];
        this.groupId = 31;
    }
    ItemComponent.prototype.getItemById = function () {
        var _this = this;
        this.item = null;
        this.itemService.getItemsById(this.itemId).subscribe(function (result) { return _this.item = result; });
    };
    ItemComponent.prototype.getItemsByGroupId = function () {
        var _this = this;
        this.items = null;
        this.itemService.getItems(this.groupId).subscribe(function (data) {
            console.log(data);
            _this.items = data;
            _this.hasItems = true;
        }, function (err) { return _this.hasItems = false; }, function () { return console.log("Success"); });
    };
    return ItemComponent;
}());
ItemComponent = __decorate([
    core_1.Component({
        selector: 'list-item',
        //templateUrl: '/partial/itemListComponent'
        templateUrl: 'item.component.html'
    }),
    __metadata("design:paramtypes", [ItemService_1.ItemService])
], ItemComponent);
exports.ItemComponent = ItemComponent;
//# sourceMappingURL=item.component.js.map