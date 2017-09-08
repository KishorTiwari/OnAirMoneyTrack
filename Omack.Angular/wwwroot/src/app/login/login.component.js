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
var http_1 = require("@angular/http");
var AuthService_1 = require("../../Services/AuthService");
var LoginComponent = (function () {
    function LoginComponent(_authService, _http) {
        this._authService = _authService;
        this._http = _http;
        //public user = new User();
        this.user = {
            email: 'kishorsanu1994@gmail.com',
            password: 'samsung44'
        };
        this.submitText = "Submit";
        this.authenticationError = "Sorry. Username and Password combination did not match.";
        this.isAuthenticated = true;
        this.showDetails = false;
        this.email = "kishorsanu1994@gmail.com";
    }
    LoginComponent.prototype.logIn = function () {
        var _this = this;
        this.isAuthenticated = true;
        this.submitText = "Submiting...";
        var body = 'email=' + this.user.email + '&password=' + this.user.password;
        this._http.post("http://localhost:52172/api/token", this.user, { headers: this._authService.contentHeaders() })
            .subscribe(function (response) {
            console.log(response.json());
            var result = response.json();
            if (result.isSuccess == true) {
                _this._authService.logIn(result.data.value);
                _this.submitText = "Submit";
            }
            else if (result.isSuccess == false) {
                console.log(result.errorMessage);
                _this.isAuthenticated = false;
                _this.authenticationError = result.errorMessage;
                _this.submitText = "Submit";
            }
        }, function (error) {
            console.log(error);
            _this.authenticationError = "Error connecting to api server";
            _this.isAuthenticated = false;
            _this.submitText = "Submit";
        });
    };
    LoginComponent.prototype.logOut = function () {
        this._authService.logOut();
    };
    LoginComponent.prototype.onClick = function () {
    };
    LoginComponent.prototype.toggleDetails = function () {
        this.showDetails = !this.showDetails;
    };
    return LoginComponent;
}());
LoginComponent = __decorate([
    core_1.Component({
        selector: 'login',
        templateUrl: 'login.component.html',
        styleUrls: ['login.component.css']
    }),
    __metadata("design:paramtypes", [AuthService_1.AuthService, http_1.Http])
], LoginComponent);
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map