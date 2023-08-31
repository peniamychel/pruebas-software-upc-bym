import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CarritoCompra } from 'src/app/entidades/carrito-compra';

@Injectable({
  providedIn: 'root'
})
export class CarritoCompraService {
  //http://localhost:5159/api/CarritoCompra/GetAllCarritoCompra
  //http://localhost:5159/api/CarritoCompra/AddCarritoCompra
  PATH_BACKEND = "http://localhost:" + "5159/";
  URL_GET = this.PATH_BACKEND + "api/CarritoCompra/";

  GET_ALL = this.URL_GET + "GetAllCarritoCompra";
  ADD = this.URL_GET+"AddCarritoCompra";
  GET_ID = "http://localhost:5159/api/CarritoCompra/GetCarritoCompraEnd";

  constructor(private httpClient: HttpClient) {

  }
  public GetAllCarritoCompra():Observable<HttpResponse<any>>{
    return this.httpClient
      .get<any>(this.GET_ALL,
        {observe: 'response'})
      .pipe();
  }

  public AddCarritoCompra(entidad: CarritoCompra): Observable<HttpResponse<any>> {
    return this.httpClient
      .post<any>(this.ADD, entidad,
        { observe: 'response' })
      .pipe();
  }

  public GetAllCarritoCompraId():Observable<HttpResponse<any>>{
    return this.httpClient
      .get<any>(this.GET_ID, {observe: 'response'})
      .pipe();
  }
  
}
