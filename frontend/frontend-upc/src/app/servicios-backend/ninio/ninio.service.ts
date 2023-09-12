import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ninio } from 'src/app/entidades/ninio';

@Injectable({
  providedIn: 'root'
})
export class NinioService {
  //http://localhost:5159/api/Ninio/GetNiniosLestTen
  //http://localhost:5159/api/Ninio/AddNinio
  //http://localhost:5159/api/Ninio/GetUsuariosById?id=1
  //http://localhost:5159/api/Ninio/DeleteNinioById?id=6
  //http://localhost:5159/api/Ninio/DeleteNinioById?id=1
  //http://localhost:5159/api/Ninio/UpdateNinio
  PATH_BACKEND = "http://localhost:" + "5159/";
  URL_GET = this.PATH_BACKEND + "api/Ninio/";

  GET_ALL = this.URL_GET + "GetNiniosLestTen";
  ADD = this.URL_GET+"AddNinio";
  GET_BY_ID = this.URL_GET+"GetNinioById";
  DELETE_BY_ID = this.URL_GET+"DeleteNinioById";
  UPDATE_BY_ID = this.URL_GET+"UpdateNinio";

  constructor(private httpClient: HttpClient) {

  }

  public GetAllNinios():Observable<HttpResponse<any>>{
    return this.httpClient
      .get<any>(this.GET_ALL,
        {observe: 'response'})
      .pipe();
  }

  public AddNinio(entidad: Ninio): Observable<HttpResponse<any>> {
    return this.httpClient
      .post<any>(this.ADD, entidad, { observe: 'response'})
      .pipe();
  }

  public getObtenerById(id: string): Observable<HttpResponse<any>> {
    var parametros = new HttpParams();
    parametros = parametros.set('id', id);

    const url = `${this.GET_BY_ID}`;
    
    return this.httpClient
      .get<any>(url, {params:parametros, observe: 'response' })
      .pipe();
  }

  public deleteById(id: number): Observable<any> {
    var parametros = new HttpParams();
    parametros = parametros.set('id', id);

    // console.log(parametros);
    //const url = `${this.DELETE_BY_ID}`;
    
    return this.httpClient
      .delete<any>(this.DELETE_BY_ID, {params:parametros, observe: 'response' })
      .pipe();
  }

  public UpdateById(entidad: Ninio): Observable<any> {
    return this.httpClient
      .patch<any>(this.UPDATE_BY_ID, entidad, { observe: 'response'})
      .pipe();
  }


}
