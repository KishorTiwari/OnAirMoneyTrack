import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { Item } from '../Models/Item';
import { AuthService } from "./AuthService";

@Injectable()
export class ItemService {
    constructor(private http: Http, private authService: AuthService) { }

    getItems(groupId: number): Observable<Item[]> {
        return this.http.get("http://localhost:52172/api/group/" + groupId +"/item/", { headers: this.authService.authJsonHeader() })
            .map((resp: Response) => resp.json());
    }

    getItemsById(itemId: number): Observable<Item> {
        return this.http.get("http://localhost:52172/api/group/44/item/" + itemId, { headers: this.authService.authJsonHeader() })
            .map((resp: Response) => resp.json());
    }
}
