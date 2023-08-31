import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuarios } from 'src/app/entidades/usuarios';


@Injectable({
  providedIn: 'root'
})
export class UsuariosService {
  //http://localhost:5159/api/Usuarios/GetTop10Usuarios
  PATH_BACKEND = "http://localhost:" + "5159/";
  URL_GET = this.PATH_BACKEND + "api/Usuarios/";

  GET_ALL = this.URL_GET + "GetTop10Usuarios";
  ADD_USUARIO = this.URL_GET+"AddUsuario";

  constructor(private httpClient: HttpClient) {

  }
  public GetAllUsuarios():Observable<HttpResponse<any>>{
    return this.httpClient
      .get<any>(this.GET_ALL,
        {observe: 'response'})
      .pipe();
  }

  public AddUsuario(entidad: Usuarios): Observable<HttpResponse<any>> {
    return this.httpClient
      .post<any>(this.ADD_USUARIO, entidad,
        { observe: 'response' })
      .pipe();
  }


}
