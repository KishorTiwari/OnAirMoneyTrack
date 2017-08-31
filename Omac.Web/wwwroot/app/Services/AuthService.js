"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var ng2_cookies_1 = require("ng2-cookies/ng2-cookies");
var AuthService = (function () {
    function AuthService() {
    }
    AuthService.prototype.authJsonHeader = function () {
        var header = new http_1.Headers();
        header.append('Content-Type', 'application/json');
        header.append('Accept', 'application/json');
        header.append('Authorization', 'bearer ' + ng2_cookies_1.Cookie.get('access_token'));
        return header;
    };
    AuthService.prototype.contentHeaders = function () {
        var header = new http_1.Headers();
        header.append('Content-Type', 'application/json');
        header.append('Accept', 'application/json');
        header.append('Access-Control-Allow-Origin', '*');
        return header;
    };
    ;
    AuthService.prototype.login = function (tokenModel) {
        var access_token = tokenModel.access_token;
        var expires_in = tokenModel.expires_in;
        ng2_cookies_1.Cookie.set('access_token', access_token, 15);
        console.log("Saved token");
    };
    ;
    AuthService.prototype.logOut = function () {
        ng2_cookies_1.Cookie.deleteAll();
        console.log("Deleted token");
    };
    return AuthService;
}());
AuthService = __decorate([
    core_1.Injectable()
], AuthService);
exports.AuthService = AuthService;
//# sourceMappingURL=AuthService.js.map