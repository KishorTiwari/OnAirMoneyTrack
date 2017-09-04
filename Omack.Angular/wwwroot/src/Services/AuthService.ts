import { Component } from '@angular/core'
import { Injectable } from '@angular/core'
import { Headers } from '@angular/http'
import { Observable } from 'rxjs/Rx'
import { Cookie } from 'ng2-cookies'
import { Token } from "../Models/Token"

@Injectable()
export class AuthService{
    authJsonHeader(): Headers {
        let header = new Headers(); 
        header.append('Content-Type', 'application/json');
        header.append('Accept', 'application/json');
        header.append('Authorization', 'bearer ' + Cookie.get('access_token'));
        return header;
    }

    contentHeaders(): Headers {
        let header = new Headers();
        header.append('Content-Type', 'application/json');
        header.append('Accept', 'application/json');
        header.append('Access-Control-Allow-Origin', '*');
        return header;
    };

    logIn(tokenModel: Token): void {
        let access_token = tokenModel.access_token;
        let expires_in = tokenModel.expires_in;
        console.log(access_token);
        Cookie.set('access_token', access_token, 15);
        //Cookie.set('expires_in', expires_in, 15);
    }

    logOut(): void {
        Cookie.deleteAll();
    }
}
