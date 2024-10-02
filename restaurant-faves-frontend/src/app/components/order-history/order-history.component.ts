import { Component } from '@angular/core';
import { Order } from '../../models/order';
import { RestaurantFavesService } from '../../services/restaurant-faves.service';
import { AddOrderFormComponent } from '../add-order-form/add-order-form.component';

@Component({
  selector: 'app-order-history',
  standalone: true,
  imports: [AddOrderFormComponent],
  templateUrl: './order-history.component.html',
  styleUrl: './order-history.component.css'
})
export class OrderHistoryComponent {
  allOrders:Order[] = [];
  constructor(private restaurantService:RestaurantFavesService) {}

  callApiGetAll(){
    this.restaurantService.getAll().subscribe(response => {
      console.log(response);
      this.allOrders = response;
    })
  }
  ngOnInit(){
    this.callApiGetAll();
  }

  callApiDeleteOrder(target:Order){
    this.restaurantService.deleteOrder(target.id).subscribe(response => this.callApiGetAll());
  }

  callApiAddOrder(newOrder:Order){
    this.restaurantService.addOrder(newOrder).subscribe(response => {
      this.callApiGetAll();
    })
  }
}
