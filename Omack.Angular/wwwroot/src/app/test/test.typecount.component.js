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
var core_1 = require("@angular/core"); //we need to decorate class properties with input decorator in order to get data form parent component
var TestTypeCountComponent = (function () {
    function TestTypeCountComponent() {
    }
    return TestTypeCountComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", Number)
], TestTypeCountComponent.prototype, "all", void 0);
__decorate([
    core_1.Input() //input decorator
    ,
    __metadata("design:type", Number)
], TestTypeCountComponent.prototype, "expensive", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", Number)
], TestTypeCountComponent.prototype, "cheap", void 0);
TestTypeCountComponent = __decorate([
    core_1.Component({
        selector: 'test-count',
        templateUrl: './test.typecount.component.html',
        styleUrls: ['./test.typecount.component.css']
    })
], TestTypeCountComponent);
exports.TestTypeCountComponent = TestTypeCountComponent;
//# sourceMappingURL=test.typecount.component.js.map