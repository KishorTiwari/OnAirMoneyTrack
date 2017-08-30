import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { Item } from '../Models/Item';

@Injectable()
export class ItemService {
    constructor(private http: Http) { }

    getItems(groupId: number): Observable<Item[]> {
        return new Item[4];
    }
}
