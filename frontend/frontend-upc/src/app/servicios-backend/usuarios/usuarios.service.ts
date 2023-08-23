import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UsuariosService {
  //http://localhost:5159/api/Usuarios/GetAllUsuarios
  PATH_BACKEND = "http://localhost:" + "5159/";
  URL_GET = this.PATH_BACKEND + "api/Usuarios/";
  GET_ALL = this.URL_GET + "GetAllUsuarios";

  constructor(private httpClient: HttpClient) {

  }
  public GetAllUsuarios():Observable<HttpResponse<any>>{
    return this.httpClient
      .get<any>(this.GET_ALL,
        {observe: 'response'})
      .pipe();
  }
}
