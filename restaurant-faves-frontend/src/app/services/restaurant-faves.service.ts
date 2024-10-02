import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class RestaurantFavesService {
  url:string = "http://localhost:5153/api/Favorites";

  constructor(private http:HttpClient) { }

  getAll(orderagain?:boolean):Observable<Order[]>{
    if(orderagain != undefined)
    {
      return this.http.get<Order[]>(this.url + '&orderagain=$(orderagain)');
    }
    else
    {
      return this.http.get<Order[]>(this.url);
    }
  }
  addOrder(o:Order):Observable<Order>{
    return this.http.post<Order>(this.url, o);
  }

  deleteOrder(id:number):Observable<void>{
    return this.http.delete<void>(this.url + `/${id}`)
  }

}
