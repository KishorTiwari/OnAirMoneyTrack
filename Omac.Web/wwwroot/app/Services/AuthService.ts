import { Component } from '@angular/core';
import { Injectable } from '@angular/core';
import { Headers } from '@angular/http';
import { Token } from "../Models/Token";
import { Observable } from 'rxjs/Rx';
import { Cookie } from 'ng2-cookies/ng2-cookies';

@Injectable()
export class AuthService {
    contentHeaders(): Headers {
        let header = new Headers();
        header.append('Content-Type', 'application/json');
        header.append('Accept', 'application/json');
        header.append('Access-Control-Allow-Origin', '*');
        return header;
    };
    login(tokenModel: Token): void {
        let access_token: string = tokenModel.access_token;
        let expires_in: number = tokenModel.expires_in;
        Cookie.set('access_token', access_token, 15);
    };
    logOut(): void {

    }
}