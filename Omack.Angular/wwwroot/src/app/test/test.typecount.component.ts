import { Component, Input } from '@angular/core';  //we need to decorate class properties with input decorator in order to get data form parent component
@Component({
    selector: 'test-count',
    templateUrl: './test.typecount.component.html',
    styleUrls: ['./test.typecount.component.css']
})
export class TestTypeCountComponent {
    @Input()
    all: number;

    @Input()  //input decorator
    expensive: number;

    @Input()
    cheap: number;
}
