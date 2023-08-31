import { UsuariosService } from './../servicios-backend/usuarios/usuarios.service';
import { Component } from '@angular/core';
import { Usuarios } from '../entidades/usuarios';
import { HttpResponse } from '@angular/common/http';


@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page {

  public nombreCompleto = "";
  public userName = "";
  public password = "";

  public listaUsuarios: Usuarios[] = [];

  constructor(private usuariosService: UsuariosService) {
    let usuario: Usuarios = new Usuarios();
    usuario.nombreCompleto = "Camilos Peredo";
    usuario.userName = "cami";
    usuario.password = "44444";

    // this.listaUsuarios.push(usuario);
    // this.listaUsuarios.push(usuario);

    this.getUsuariosFromBackend();
  }

  private getUsuariosFromBackend(){
    this.usuariosService.GetAllUsuarios().subscribe({
      next: (response: HttpResponse<any>) => {
        this.listaUsuarios = response.body;
        // console.log(this.listaUsuarios)
      },
      error: (error: any) => {
        // falla
        console.log(error);
      },
      complete: () => {
        // termino todo
        console.log('Complete - this.getUsuarios() BIEN ECHO');
      },
    });
  }

  public addUsuario(){
    console.log(this.nombreCompleto);
    console.log(this.userName);
    console.log(this.password);
    this.AddUsuarioBackend(this.nombreCompleto, this.userName, this.password);
  }

  private AddUsuarioBackend(nombreCompleto: string, userName: string, password:string){

    var usuarioEntidad = new Usuarios();
    usuarioEntidad.nombreCompleto = nombreCompleto;
    usuarioEntidad.userName = userName;
    usuarioEntidad.password = password;

    this.usuariosService.AddUsuario(usuarioEntidad).subscribe({
      next: (response: HttpResponse<any>)=> {
        console.log(response.body);
        if(response.body == -1){
          alert("Se agrego el USUARIO con exito :)");
          this.getUsuariosFromBackend();//Se actualize el listado
          this.nombreCompleto = "";
          this.userName = "";
          this.password = "";
          //quiero crear un retraso de 1 segundo en esta parte del codigo
          setTimeout(() => {
            alert("Se retraso :)");
          }, 1000);
      }else{
          alert("Al agregar al USUARIO fallo :(");
      }
      },
      error: (error: any) => {
        console.log(error);
      },
      complete: ()=> {
        console.log('Fue completado  - this.AddUsuario :)');
      },
    });

  }

}
