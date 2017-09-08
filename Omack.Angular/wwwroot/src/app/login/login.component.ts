import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Response } from '@angular/http';
import { AuthService } from '../../Services/AuthService';
import { User } from "../../Models/User";

@Component({
    selector: 'login',
    templateUrl: 'login.component.html',
    styleUrls: ['login.component.css']
})

export class LoginComponent {
    constructor(private _authService: AuthService, public _http: Http) { }
    //public user = new User();
    public user: User = {
        email: 'kishorsanu1994@gmail.com',
        password: 'samsung44'
    };
    submitText: string = "Submit";
    authenticationError: string = "Sorry. Username and Password combination did not match.";
    isAuthenticated: boolean = true;
    logIn(): void {
        this.isAuthenticated = true;
        this.submitText = "Submiting...";
        let body = 'email=' + this.user.email + '&password=' + this.user.password;
        this._http.post("http://localhost:52172/api/token", this.user, { headers: this._authService.contentHeaders() })
            .subscribe(response => {
                console.log(response.json());
                var result = response.json();
                if (result.isSuccess == true) {
                    this._authService.logIn(result.data.value);
                    this.submitText = "Submit";
                }
                else if (result.isSuccess == false) {
                    console.log(result.errorMessage);
                    this.isAuthenticated = false;
                    this.authenticationError = result.errorMessage;
                    this.submitText = "Submit";
                }
            },
            error => {
                console.log(error);
                this.authenticationError = "Error connecting to api server";
                this.isAuthenticated = false;
                this.submitText = "Submit";
            }
            );
    }

    logOut(): void {
        this._authService.logOut();
    }

    onClick(): void {
    }

    showDetails: boolean = false;
    toggleDetails(): void {
        this.showDetails = !this.showDetails;
    }

    email: string = "kishorsanu1994@gmail.com";

    //applyBtnClass: boolean = true;
    //applyBtnLgClass: boolean = true;

    //applyAllClass() {
    //    let classes = {
    //        'btn': this.applyBtnClass,
    //        'btn-lg' : this.applyBtnLgClass
    //    }
    //    return classes;
    //}


    //isBold: boolean = true;
    //fontSize: number = 30;
    //fontSizePx: string = '30px';
    //addStyles() {
    //    let styles = {
    //        'font-weight': this.isBold? 'bold': 'normal',
    //        'font-size.px': this.fontSize
    //    }
    //    return styles;
    //}
}
