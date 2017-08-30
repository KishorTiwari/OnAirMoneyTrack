import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { LoginComponent } from './Login/LoginComponent';
import { HttpModule } from "@angular/http";
import { AuthService } from "./Services/AuthService";

@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule],
    declarations: [AppComponent, LoginComponent],
    providers: [
        AuthService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
