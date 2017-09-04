import { Component} from '@angular/core';

@Component({
    selector: 'test',
    template: `<h1>Test Component: {{title}}</h1>`
})

export class TestComponent {
    title: string = "This is test component";
    bannerTitle: string = "This is banner title";
}
