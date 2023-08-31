import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CategoriaProducto } from 'src/app/entidades/categoria-producto';

@Injectable({
  providedIn: 'root'
})
export class CategoriaProductoService {

  PATH_BACKEND = "http://localhost:" + "5159/";
  URL_GET = this.PATH_BACKEND + "api/CategoriaProducto/";

  GET_ALL = this.URL_GET + "GetAllCategoriaProducto";
  ADD_USUARIO = this.URL_GET+"AddCategoriaProducto";

  constructor(private httpClient: HttpClient) {

  }
  public GetAllCategoriaProducto():Observable<HttpResponse<any>>{
    return this.httpClient
      .get<any>(this.GET_ALL,
        {observe: 'response'})
      .pipe();
  }

  public AddCategoriaProducto(entidad: CategoriaProducto): Observable<HttpResponse<any>> {
    return this.httpClient
      .post<any>(this.ADD_USUARIO, entidad,
        { observe: 'response' })
      .pipe();
  }
}
