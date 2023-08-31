import { Component, OnInit } from '@angular/core';
import { DejaEnGuarderia } from '../entidades/deja-en-guarderia';
import { DejaEnGuarderiaService } from '../servicios-backend/deja-en-guarderia/deja-en-guarderia.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-tab6',
  templateUrl: './tab6.page.html',
  styleUrls: ['./tab6.page.scss'],
})
export class Tab6Page implements OnInit {

  public idUsuario = '';
  public idNinio = '';
  public horaEntrada: Date = new Date();
  public horaSalida: Date = new Date();


  public listaDejaEnGuarderia: DejaEnGuarderia[] = [];

  constructor(private dejaEnGuarderiaService: DejaEnGuarderiaService) {

    this.getDejaEnGuarderia();
  }

  public getDejaEnGuarderia() {
    this.dejaEnGuarderiaService.GetLastTen().subscribe({
      next: (response: HttpResponse<any>) => {
        this.listaDejaEnGuarderia = response.body;
        console.log(this.listaDejaEnGuarderia);
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

  public addDejaEnGuarderia(){
    this.addDejaEnGuarderiax(this.idUsuario, this.idNinio);
  }

  private addDejaEnGuarderiax(idUsuario: string, idNinio: string){

    var dejaEnGuarderia = new DejaEnGuarderia();
    dejaEnGuarderia.idUsuario = parseInt(idUsuario);
    dejaEnGuarderia.idNinio = parseInt(idNinio);

    this.dejaEnGuarderiaService.AddDejaEnGuarderia(dejaEnGuarderia).subscribe({
      next: (response: HttpResponse<any>)=> {
        console.log(response.body);
        if(response.body == 1){
          alert("Se agrego el USUARIO con exito :)");
          this.getDejaEnGuarderia();//Se actualize el listado
          this.idNinio = "";
          this.idUsuario = "";
          // this.horaEntrada = "";
          //quiero crear un retraso de 1 segundo en esta parte del codigo
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

  ngOnInit() {
  }

}
