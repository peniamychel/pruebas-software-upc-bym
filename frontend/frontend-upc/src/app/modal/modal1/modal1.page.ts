import { HttpResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { convertToParamMap } from '@angular/router';
import { ModalController } from '@ionic/angular';
import { Ninio } from 'src/app/entidades/ninio';
import { NinioService } from 'src/app/servicios-backend/ninio/ninio.service';

@Component({
  selector: 'app-modal1',
  templateUrl: './modal1.page.html',
  styleUrls: ['./modal1.page.scss'],
})
export class Modal1Page implements OnInit {

  @Input() idNinio: any;
  public nombre = "";
  public edad = "";
  public sexo = "";

  public ninio: Ninio = new Ninio();

  constructor(private ninioService: NinioService, private modalCtrl: ModalController) {

  }

  ngOnInit() {
    this.ninioService.getObtenerById(this.idNinio).subscribe({
      next: (response: HttpResponse<any>) => {
        // alert(JSON.stringify(response.body));
        this.ninio = response.body;
        console.log(JSON.stringify(response.body));
        this.nombre = this.ninio.nombre;
        this.edad = this.ninio.edad+"";
        this.sexo = this.ninio.sexo;
      },
      error: (error: any) => {
        // falla
        console.log(error);
      },
      complete: () => {
        // termino todo
        console.log('Finished... - this.darDatos()');
      },
    });

  }

  public botonCancelar(){
    this.modalCtrl.dismiss();
  }

  public actualizarDatos(){
    var aux  = false;

    this.ninio.nombre = this.nombre;
    this.ninio.edad = parseInt(this.edad);
    this.ninio.sexo = this.sexo;

    this.ninioService.UpdateById(this.ninio).subscribe({
      next: (response: HttpResponse<any>)=> {
        // console.log(response.body);
      },
      error: (error: any) => {
        console.log(error);
      },
      complete: ()=> {
        console.log('Finished...  - this.addNinios :)');
        aux = true;
      },
    });
    this.modalCtrl.dismiss(aux);
  }

}
