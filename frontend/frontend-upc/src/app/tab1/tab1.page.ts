import { UsuariosService } from './../servicios-backend/usuarios/usuarios.service';
import { Component } from '@angular/core';
import { Usuarios } from '../entidades/Usuarios';
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

    this.getUsuario();
  }

  private getUsuario(){
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

  }

}
