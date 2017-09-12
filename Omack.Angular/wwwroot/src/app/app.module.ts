import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'

import { AppComponent } from './app.component';
import { LoginComponent } from "./login/login.component";
import { HttpModule } from "@angular/http";
import { AuthService } from "../Services/AuthService";
import { ItemService } from "../Services/ItemService";
import { GroupService } from "../Services/GroupService";
import { ItemComponent } from './item/item.component';
import { EmailValidator } from '@angular/forms'
import { TestComponent } from "./test/test.component";
import { GroupComponent } from './group/group.component';
import { ItemTypePipe } from "./test/test.pipe";
import { TestTypeCountComponent } from "./test/test.typecount.component";


@NgModule({
    declarations: [
        AppComponent, LoginComponent, ItemComponent, TestComponent, ItemTypePipe, TestTypeCountComponent, GroupComponent
  ],
  imports: [
      BrowserModule, FormsModule, HttpModule
  ],
  providers: [AuthService, ItemService, GroupService],
  bootstrap: [AppComponent]
})
export class AppModule { }
