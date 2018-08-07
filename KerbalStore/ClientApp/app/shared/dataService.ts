import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map'
import { Observable } from 'rxjs/Observable';
import { RocketPart } from './rocketPart';
import { Order, OrderItem } from './order';
import { ILogin } from './login';

@Injectable()
export class DataService {
    
    constructor(private http: HttpClient) {

    }

    loadProducts(): Observable<boolean> {
        return this.http.get("/api/RocketPart")
            .map((data: any[]) => {
                this.RocketParts = data;
                return true;
            });
    }

    login(username:string, password:string): Observable<ILogin> {
        return this.http.post("/api/Login", {
            username: username,
            password: password
        }).map((tokenDetails: ILogin) => {
            this.token = tokenDetails.token;
            this.tokenExpiry = tokenDetails.tokenExpiry;
            return tokenDetails;
        });
    }

    public RocketParts: RocketPart[] = [];

    public Order: Order = new Order();

    public addToOrder(newRocketPart: RocketPart) {

        var existingOrderItem = this.Order.orderItems.find(oi => oi.rocketPartId == newRocketPart.id);
        if (existingOrderItem) {
            existingOrderItem.quantity++;
        } else {

            var newOrderItem = new OrderItem();
            newOrderItem.quantity = 1;
            newOrderItem.rocketPartId = newRocketPart.id;
            newOrderItem.rocketPartPartName = newRocketPart.partName;
            newOrderItem.unitPrice = newRocketPart.price;
            this.Order.orderItems.push(newOrderItem);
        }
    }

    public removeFromOrder(orderItemToRemove: OrderItem) {
        this.Order.orderItems = this.Order.orderItems.filter((existingOrderItem, array) => {
            return !(existingOrderItem == orderItemToRemove);
        });
    }


    private token: string = "";
    private tokenExpiry: Date = new Date();

    loginRequired(): boolean {
        return this.token.length == 0 || this.tokenExpiry < new Date();
    }


}