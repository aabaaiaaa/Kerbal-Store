import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'
import { RouterModule } from '@angular/router'
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { RocketPartList } from './shop/rocketPartList.component'
import { Kart } from './shop/kart.component'
import { Shop } from './shop/shop.component';
import { Checkout } from './checkout/checkout.component';
import { Login } from './login/login.component';

import { DataService } from './shared/dataService'

let routes = [{
    path: "", component: Shop
}, {
    path: "checkout", component: Checkout
}, {
    path: "login", component: Login
}];

@NgModule({
    declarations: [
        AppComponent,
        RocketPartList,
        Kart,
        Shop,
        Checkout,
        Login
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        RouterModule.forRoot(routes, {
            useHash: true,
            enableTracing: true
        }),
        FormsModule
    ],
    providers: [
        DataService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
