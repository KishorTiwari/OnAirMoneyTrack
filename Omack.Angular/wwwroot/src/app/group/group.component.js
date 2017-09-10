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
var GroupService_1 = require("../../Services/GroupService");
var GroupComponent = (function () {
    function GroupComponent(groupService) {
        this.groupService = groupService;
        this.groups = [];
        this.group = null;
        this.groupId = null;
        this.hasGroup = false;
        this.hasGroups = false;
    }
    GroupComponent.prototype.ngOnInit = function () {
        this.getGroups();
    };
    GroupComponent.prototype.getGroupById = function () {
        var _this = this;
        this.group = null;
        this.groupService.getGroupById(this.groupId).subscribe(function (result) { return _this.group = result; });
    };
    GroupComponent.prototype.getGroups = function () {
        var _this = this;
        this.groups = null;
        this.groupService.getGroup(this.groupId).subscribe(function (data) {
            console.log(data);
            _this.groups = data;
            _this.hasGroups = true;
        }, function (err) { return _this.hasGroups = false; }, function () { return console.log("Success"); });
    };
    return GroupComponent;
}());
GroupComponent = __decorate([
    core_1.Component({
        selector: 'group-list',
        //templateUrl: '/partial/itemListComponent'
        templateUrl: 'group.component.html'
    }),
    __metadata("design:paramtypes", [GroupService_1.GroupService])
], GroupComponent);
exports.GroupComponent = GroupComponent;
//# sourceMappingURL=group.component.js.map