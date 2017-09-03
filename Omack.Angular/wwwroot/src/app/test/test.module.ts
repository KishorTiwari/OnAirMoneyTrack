import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { TestComponent } from './test.component';

@NgModule({
    declarations:[TestComponent],
    imports: [BrowserModule, FormsModule],
    providers: [],
    bootstrap: [TestComponent]
})
export class TestModule { }
