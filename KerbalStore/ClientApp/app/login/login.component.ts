import { Component } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Router } from "@angular/router";


@Component({
    templateUrl: "login.component.html"
})
export class Login {
    private username: string;
    private password: string;
    private rememberMe: boolean;

    private errorMessage: string = "";

    constructor(private data: DataService, private router: Router) { }

    onLogin() {

        this.data.login(this.username, this.password).subscribe((tokenDetails) => {
            this.router.navigate([(this.data.Order.orderItems.length > 0) ? "checkout" : ""]);
        }, err => this.errorMessage = "Invalid login credentials");
    }
}