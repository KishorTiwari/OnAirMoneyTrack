import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'

import { AppComponent } from './app.component';
import { LoginComponent } from "./login/login.component";
import { HttpModule } from "@angular/http";
import { AuthService } from "../Services/AuthService";
import { ItemService } from "../Services/ItemService";
import { ItemComponent } from './item/item.component';
import { EmailValidator } from '@angular/forms'

@NgModule({
    declarations: [
        AppComponent, LoginComponent, ItemComponent
  ],
  imports: [
      BrowserModule, FormsModule, HttpModule
  ],
  providers: [AuthService, ItemService],
  bootstrap: [AppComponent]
})
export class AppModule { }
