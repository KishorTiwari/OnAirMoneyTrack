import { Component, OnInit } from '@angular/core';
import { Group } from "../../Models/Group";
import { GroupService } from "../../Services/GroupService";

@Component({
    selector: 'group-list',
    //templateUrl: '/partial/itemListComponent'
    templateUrl: 'group.component.html'
})

export class GroupComponent implements OnInit {
    groups: Group[] = [];
    ngOnInit(): void {
        this.getGroups();
    }
    constructor(private groupService: GroupService) { }

    group: Group = null;
    groupId: number = null;
    hasGroup: boolean = false;

    hasGroups: boolean = false;


    getGroupById(): void {
        this.group = null;
        this.groupService.getGroupById(this.groupId).subscribe((result: Group) => this.group = result);
    }

    getGroups(): void {
        this.groups = null;
        this.groupService.getGroup(this.groupId).subscribe(
            (data: Group[]) => {
                console.log(data);
                this.groups = data;
                this.hasGroups = true;
            },
            err => this.hasGroups = false,
            () => console.log("Success")
        );
    }
}
