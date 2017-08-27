import { Component } from '@angular/core';

@Component({
  selector: 'my-app',
  template: `<h1>Hello {{name}}</h1>`, //backtick for multiple line
})
export class AppComponent  { name = 'Angular 2'; }
