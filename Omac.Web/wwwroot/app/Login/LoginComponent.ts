import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Http } from '@angular/http';
import { AuthService } from '../Services/AuthService';
import { User } from "../Models/User";

@Component({
    selector: 'login',
    templateUrl: '/partial/loginComponent',
})

export class LoginComponent {
    constructor(private _authService: AuthService, public _http: Http) { }
    //public user = new User();
    public user: User = {
        email: 'kishorsanu1994@gmail.com',
        password: 'samsung44'
    };

    login(): void {
        let body = 'email=' + this.user.email + '&password=' + this.user.password;
        this._http.post("http://localhost:52172/api/token", this.user, { headers: this._authService.contentHeaders() })
            .subscribe(response => {
                this._authService.login(response.json());
            });
    }

    logout(): void {
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