import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { LoginComponent } from './Login/LoginComponent';
import { HttpModule } from "@angular/http";
import { AuthService } from "./Services/AuthService";
import { ItemService } from "./Services/ItemService";
import { ItemComponent } from "./Item/item.component";

@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule],
    declarations: [AppComponent, LoginComponent, ItemComponent],
    providers: [
        AuthService,
        ItemService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
