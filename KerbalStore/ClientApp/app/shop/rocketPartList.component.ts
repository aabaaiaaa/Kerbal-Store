import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/dataService'
import { RocketPart } from '../shared/rocketPart'

@Component({
    selector: "rocket-part-list",
    templateUrl: "rocketPartList.component.html",
    styleUrls: []
})
export class RocketPartList implements OnInit {
    ngOnInit(): void {
        this.data.loadProducts()
            .subscribe(success => {
                if(success) this.RocketParts = this.data.RocketParts;
            });
    }
    
    constructor(private data: DataService) { }

    public RocketParts: RocketPart[] = [];

    public addToKart(newRocketPart: RocketPart) {
        this.data.addToOrder(newRocketPart);
    }
}