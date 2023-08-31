import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Producto } from 'src/app/entidades/producto';

@Injectable({
  providedIn: 'root',
})
export class ProductoService {
  //http://localhost:5159/api/Producto/GetAllProductos
  PATH_BACKEND = 'http://localhost:' + '5159/';
  URL_GET = this.PATH_BACKEND + 'api/Producto/';

  GET_ALL = this.URL_GET + 'GetAllProductos';
  ADD = this.URL_GET + 'AddProducto';

  constructor(private httpClient: HttpClient) {}
  public GetAllProducto(): Observable<HttpResponse<any>> {
    return this.httpClient
      .get<any>(this.GET_ALL, { observe: 'response' })
      .pipe();
  }

  public AddProcuto(entidad: Producto): Observable<HttpResponse<any>> {
    return this.httpClient
      .post<any>(this.ADD, entidad, { observe: 'response'})
      .pipe();
  }

  // public eliminarProducto(id: number): Observable<HttpResponse<any>> {
  //   const url = `${this.DELETE}/${id}`;
  //   return this.httpClient.delete<any>(url, { observe: 'response' }).pipe();
  // }
}
