import { Component } from '@angular/core';
import { NgModule } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
    public firstName: string = 'Kishor';
    public lastName: string = 'Tiwari';

    changeMe(): void {
        this.firstName = 'Kamal';
        this.lastName = 'Lamgade';
    }
}
