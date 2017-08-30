import { Component } from '@angular/core';

@Component({
  selector: 'my-app',
  template: `<h1>Hello {{name}}</h1><div><login></login></div>`, //backtick for multiple line
})
export class AppComponent  { name = 'Angular 2'; }
