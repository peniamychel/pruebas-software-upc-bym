import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NinioService } from '../servicios-backend/ninio/ninio.service';
import { HttpResponse } from '@angular/common/http';
import { Ninio } from '../entidades/ninio';

@Component({
  selector: 'app-editer-ninio',
  templateUrl: './editer-ninio.page.html',
  styleUrls: ['./editer-ninio.page.scss'],
})
export class EditerNinioPage implements OnInit {
  public nombre = "";
  public edad = "";
  public sexo = "";

  public ninio: Ninio = new Ninio();

  constructor(private ninioService: NinioService, private acitivateRoute: ActivatedRoute, router: Router) { }

  ngOnInit() {
    this.acitivateRoute.paramMap.subscribe((paramMat)=> {
      var idd =paramMat.get('id');
      if(paramMat.get('id')){
        this.ninioService.getObtenerById(idd+'').subscribe({
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
      console.log(paramMat.get('id'))
    })
  }

  public actualizarDatos(){

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
      },
    });
  }

}
