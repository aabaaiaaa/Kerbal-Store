export class Order {
    orderId: number;
    orderDate: Date = new Date();
    orderRef: string;
    orderItems: Array<OrderItem> = new Array<OrderItem>();

    get subtotal(): number {
        var orderItemCosts = this.orderItems.map((orderItem, index, array) => {
            return orderItem.quantity * orderItem.unitPrice;
        });
        return (orderItemCosts.length > 0) ? orderItemCosts.reduce((previous, current) => {
            return previous + current;
        }) : 0;
    }
}

export class OrderItem {
    id: number;
    quantity: number;
    unitPrice: number;
    rocketPartId: number;
    rocketPartPartName: string;
}