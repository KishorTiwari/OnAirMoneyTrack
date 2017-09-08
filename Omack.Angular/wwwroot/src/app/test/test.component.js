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
var TestComponent = (function () {
    //dependencty injection via constructor
    function TestComponent(itemService) {
        this.itemService = itemService;
        this.itemId = 46;
        this.groupId = 31;
        //itemService.getItems(this.groupId).subscribe((result: Item[]) => this.items = result);
    }
    //runs after all the dependencies, functions and properties of component are initialized.
    TestComponent.prototype.ngOnInit = function () {
        this.getItemsByGroupId();
    };
    //methods
    TestComponent.prototype.getItemById = function () {
        var _this = this;
        this.item = null;
        this.itemService.getItemsById(this.itemId).subscribe(function (result) { return _this.item = result; });
    };
    TestComponent.prototype.getItemsByGroupId = function () {
        var _this = this;
        this.itemService.getItems(this.groupId).subscribe(function (data) {
            _this.items = data; //set property value
        }, function (err) { _this.items = null; }, //catch error
        function () { return console.log("Success"); } //function complete
        );
    };
    TestComponent.prototype.trackByItemId = function (index, item) {
        return item.id;
    };
    TestComponent.prototype.getTotalCount = function () {
        return this.items.length;
    };
    TestComponent.prototype.getTotalExpensiveCount = function () {
        return this.items.filter(function (x) { return x.price >= 100; }).length;
    };
    TestComponent.prototype.getTotalCheapCount = function () {
        return this.items.filter(function (x) { return x.price < 100; }).length;
    };
    return TestComponent;
}());
TestComponent = __decorate([
    core_1.Component({
        selector: 'test',
        templateUrl: 'test.component.html',
        styleUrls: ['test.component.css']
    }),
    __metadata("design:paramtypes", [ItemService_1.ItemService])
], TestComponent);
exports.TestComponent = TestComponent;
//# sourceMappingURL=test.component.js.map