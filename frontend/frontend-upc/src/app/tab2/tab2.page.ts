import { Component } from '@angular/core';
import { CategoriaProducto } from '../entidades/categoria-producto';
import { CategoriaProductoService } from '../servicios-backend/categoria-producto/categoria-producto.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss']
})
export class Tab2Page {
  public nombre = "";

  public listaCategoriaProducto: CategoriaProducto[] = [];

  constructor(private categoriaProductoService: CategoriaProductoService) {
    let categoriaProducto: CategoriaProducto = new CategoriaProducto();
    categoriaProducto.nombre = "Camilos Peredo";

    this.getCategoriaProductoFromBackend();
  }

  private getCategoriaProductoFromBackend(){
    this.categoriaProductoService.GetAllCategoriaProducto().subscribe({
      next: (response: HttpResponse<any>) => {
        this.listaCategoriaProducto = response.body;
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

  public addCategoriaProducto(){
    console.log(this.nombre);
    this.AddCategoriaProducto(this.nombre);
  }

  private AddCategoriaProducto(nombre: string){

    var categoriaProductoEntidad = new CategoriaProducto();
    categoriaProductoEntidad.nombre = nombre;
  

    this.categoriaProductoService.AddCategoriaProducto(categoriaProductoEntidad).subscribe({
      next: (response: HttpResponse<any>)=> {
        console.log(response.body);
        if(response.body == 1){
          alert("Se agrego la categoriaProductoService con exito :)");
          this.getCategoriaProductoFromBackend();//Se actualize el listado
          this.nombre = "";
      }else{
          alert("Al agregar al categoriaProductoService fallo :(");
      }
      },
      error: (error: any) => {
        console.log(error);
      },
      complete: ()=> {
        console.log('Fue completado  - this.CategoriaProductoService :)');
      },
    });

  }

}
