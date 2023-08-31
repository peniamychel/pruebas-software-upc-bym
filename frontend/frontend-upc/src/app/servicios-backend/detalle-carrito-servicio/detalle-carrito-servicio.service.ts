import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DetalleCarrito } from 'src/app/entidades/detalle-carrito';

@Injectable({
  providedIn: 'root'
})
export class DetalleCarritoServicioService {

  //http://localhost:5159/api/DetalleCarrito/GetAllDetalleCarrito
  PATH_BACKEND = "http://localhost:" + "5159/";
  URL_GET = this.PATH_BACKEND + "api/DetalleCarrito/";

  GET_ALL = this.URL_GET + "GetAllDetalleCarrito";
  ADD = this.URL_GET+"AddDetalleCarrito";

  constructor(private httpClient: HttpClient) {

  }
  public GetAllDetalleCarrito():Observable<HttpResponse<any>>{
    return this.httpClient
      .get<any>(this.GET_ALL,
        {observe: 'response'})
      .pipe();
  }

  public AddDetalleCarrito(entidad: DetalleCarrito): Observable<HttpResponse<any>> {
    return this.httpClient
      .post<any>(this.ADD, entidad,
        { observe: 'response' })
      .pipe();
  }
}
