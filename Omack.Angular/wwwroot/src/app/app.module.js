"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var app_component_1 = require("./app.component");
var login_component_1 = require("./login/login.component");
var http_1 = require("@angular/http");
var AuthService_1 = require("../Services/AuthService");
var ItemService_1 = require("../Services/ItemService");
var item_component_1 = require("./item/item.component");
var test_component_1 = require("./test/test.component");
var test_pipe_1 = require("./test/test.pipe");
var test_typecount_component_1 = require("./test/test.typecount.component");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        declarations: [
            app_component_1.AppComponent, login_component_1.LoginComponent, item_component_1.ItemComponent, test_component_1.TestComponent, test_pipe_1.ItemTypePipe, test_typecount_component_1.TestTypeCountComponent
        ],
        imports: [
            platform_browser_1.BrowserModule, forms_1.FormsModule, http_1.HttpModule
        ],
        providers: [AuthService_1.AuthService, ItemService_1.ItemService],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map