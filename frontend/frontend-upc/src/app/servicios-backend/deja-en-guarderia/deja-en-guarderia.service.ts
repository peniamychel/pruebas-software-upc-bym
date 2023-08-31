import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DejaEnGuarderia } from 'src/app/entidades/deja-en-guarderia';

@Injectable({
  providedIn: 'root'
})
export class DejaEnGuarderiaService {

  //http://localhost:5159/api/DejaEnGuarderia/GetLastTen
  //http://localhost:5159/api/DejaEnGuarderia/AddDejaEnGuarderia
  PATH_BACKEND = "http://localhost:" + "5159/";
  URL_GET = this.PATH_BACKEND + "api/DejaEnGuarderia/";

  GET_ALL = this.URL_GET + "GetLastTen";
  ADD = this.URL_GET+"AddDejaEnGuarderia";

  constructor(private httpClient: HttpClient) {

  }
  public GetLastTen():Observable<HttpResponse<any>>{
    return this.httpClient
      .get<any>(this.GET_ALL,
        {observe: 'response'})
      .pipe();
  }

  public AddDejaEnGuarderia(entidad: DejaEnGuarderia): Observable<HttpResponse<any>> {
    return this.httpClient
      .post<any>(this.ADD, entidad,
        { observe: 'response' })
      .pipe();
  }
}
