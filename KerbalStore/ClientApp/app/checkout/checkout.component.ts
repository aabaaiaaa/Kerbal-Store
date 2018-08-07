import { Component } from '@angular/core';
import { DataService } from '../shared/dataService';


@Component({
    selector: "checkout",
    templateUrl: "checkout.component.html"
})
export class Checkout {
    constructor(private data: DataService) {

    }
}