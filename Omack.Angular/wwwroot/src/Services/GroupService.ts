import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { Group } from '../Models/Group';
import { AuthService } from "./AuthService";

@Injectable()
export class GroupService {
    constructor(private http: Http, private authService: AuthService) { }

    getGroup(groupId: number): Observable<Group[]> {
        return this.http.get("http://localhost:52172/api/group/", { headers: this.authService.authJsonHeader() })
            .map((resp: Response) => resp.json());
    }

    getGroupById(groupId: number): Observable<Group> {
        return this.http.get("http://localhost:52172/api/group/" + groupId, { headers: this.authService.authJsonHeader() })
            .map((resp: Response) => resp.json());
    }
}
