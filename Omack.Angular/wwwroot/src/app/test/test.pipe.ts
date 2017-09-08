import {Pipe, PipeTransform } from "@angular/core";  //pipe for pipe decorator , pipetransform for interface
@Pipe({
    name: 'itemType'
})
export class ItemTypePipe implements PipeTransform {
    transform(value: string, price: number): string {
        if (price > 100) {
            return "Expensive" + value;
        }
        else {
            return "Cheap" + value;
        }
    }
}
