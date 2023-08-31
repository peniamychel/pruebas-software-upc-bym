import { Component } from '@angular/core';
import { Producto } from '../entidades/producto';
import { ProductoService } from '../servicios-backend/producto/producto.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-tab3',
  templateUrl: 'tab3.page.html',
  styleUrls: ['tab3.page.scss']
})
export class Tab3Page {

  public nombreProducto = "";
  public idCategoria = "";

  public listaProductos: Producto[] = [];

  constructor(private productoService: ProductoService) {
    let producto: Producto = new Producto();
    producto.nombre = "Informaticos";
    producto.idCategoria = 11;

    // this.listaUsuarios.push(usuario);
    // this.listaUsuarios.push(usuario);

    this.getProductosFromBackend();
  }

  private getProductosFromBackend(){
    this.productoService.GetAllProducto().subscribe({
      next: (response: HttpResponse<any>) => {
        this.listaProductos = response.body;
        // console.log(this.listaUsuarios)
      },
      error: (error: any) => {
        // falla
        console.log(error);
      },
      complete: () => {
        // termino todo
        console.log('Complete - this.getProductos() BIEN ECHO');
      },
    });
  }

  public addProducto(){
    console.log(this.nombreProducto);
    console.log(parseInt(this.idCategoria));
    
    this.AddCategoriaProducto(this.nombreProducto, parseInt(this.idCategoria));
  }

  private AddCategoriaProducto(nombre: string, idCategoria: number){

    var categoriaProductoEntidad = new Producto();
    categoriaProductoEntidad.nombre = nombre;
    categoriaProductoEntidad.idCategoria = idCategoria;

    this.productoService.AddProcuto(categoriaProductoEntidad).subscribe({
      next: (response: HttpResponse<any>)=> {
        console.log(response.body);
        if(response.body == 1){
          alert("Se agrego la categoriaProductoService con exito :)");
          this.getProductosFromBackend();//Se actualize el listado
          this.nombreProducto = "";
          this.idCategoria = "";
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
