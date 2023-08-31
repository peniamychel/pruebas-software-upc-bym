import { Component, OnInit } from '@angular/core';
import { Ninio } from '../entidades/ninio';
import { NinioService } from '../servicios-backend/ninio/ninio.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-tab5',
  templateUrl: './tab5.page.html',
  styleUrls: ['./tab5.page.scss'],
})
export class Tab5Page implements OnInit {
  public nombre = '';
  public edad = '';
  public sexo = '';

  public listaNinios: Ninio[] = [];

  constructor(private ninioService: NinioService) {
    this.getNinios();
  }

  public getNinios() {
    this.ninioService.GetAllNinios().subscribe({
      next: (response: HttpResponse<any>) => {
        this.listaNinios = response.body;
        // console.log(this.listaNinios);
      },
      error: (error: any) => {
        // falla
        console.log(error);
      },
      complete: () => {
        // termino todo
        console.log('Complete... - this.getNinios()');
      },
    });
  }

  public addNinio(){
    this.addNinios(this.nombre, this.edad, this.sexo);
  }

  private addNinios(nombre: string, edad: string, sexo:string){

    var ninioEntidad = new Ninio();
    ninioEntidad.nombre = nombre;
    ninioEntidad.edad = parseInt(edad);
    ninioEntidad.sexo = sexo;

    this.ninioService.AddNinio(ninioEntidad).subscribe({
      next: (response: HttpResponse<any>)=> {
        console.log(response.body);
        if(response.body == 1){
          alert("Se agrego el USUARIO con exito :)");
          this.getNinios();//Se actualize el listado
          this.nombre = "";
          this.edad = "";
          this.sexo = "";
          //quiero crear un retraso de 1 segundo en esta parte del codigo
      }else{
          alert("Al agregar al USUARIO fallo :(");
      }
      },
      error: (error: any) => {
        console.log(error);
      },
      complete: ()=> {
        console.log('Finished...  - this.addNinios :)');
      },
    });

  }

  public darDatos(id: number){
    console.log(id);
    this.ninioService.getObtenerById(id).subscribe({
      next: (response: HttpResponse<any>) => {
        alert(JSON.stringify(response.body));
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

  public deleteById(id: number){
    console.log(id);
    this.ninioService.deleteById(id).subscribe({
      next: (response: any) => {
        // console.log(response);
        alert(JSON.stringify(response.body));
        this.getNinios();
      },
      error: (error: any) => {
        // falla
        console.log(error);
      },
      complete: () => {
        // termino todo
        console.log('Finished... - this.deleteById()');
      },
    });

  }
  public updateById(id: number){
    console.log(id);
    this.ninioService.deleteById(id).subscribe({
      next: (response: any) => {
        // console.log(response);
        alert(JSON.stringify(response.body));
        this.getNinios();
      },
      error: (error: any) => {
        // falla
        console.log(error);
      },
      complete: () => {
        // termino todo
        console.log('Finished... - this.deleteById()');
      },
    });

  }

  ngOnInit() {
  }

}
