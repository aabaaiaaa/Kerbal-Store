import { Component } from '@angular/core';
import { DataService } from '../shared/dataService'
import { OrderItem } from '../shared/order';
import { Router } from '@angular/router';

@Component({
    selector: "kerbal-kart",
    templateUrl: "kart.component.html",
    styleUrls: []
})
export class Kart {

    constructor(private data: DataService, private router: Router) { }

    public removeOrderItem(orderItem: OrderItem) {
        this.data.removeFromOrder(orderItem);
    }

    onNavigate() {
        if (this.data.loginRequired()) {
            // Nav to login component
            this.router.navigate(["login"]);
        } else {
            // Nav to checkout
            this.router.navigate(["checkout"]);
        }
    }
}