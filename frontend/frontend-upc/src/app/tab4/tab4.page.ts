import { Observable } from 'rxjs';
import { AuxDetalleProducto } from './../entidades/aux-detalle-producto';
import { DetalleCarritoServicioService } from './../servicios-backend/detalle-carrito-servicio/detalle-carrito-servicio.service';
import { DetalleCarrito } from 'src/app/entidades/detalle-carrito';
import { CarritoCompraService } from './../servicios-backend/carrito-compra/carrito-compra.service';
import { Component, OnInit } from '@angular/core';
import { CarritoCompra } from '../entidades/carrito-compra';
import { Producto } from '../entidades/producto';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-tab4',
  templateUrl: './tab4.page.html',
  styleUrls: ['./tab4.page.scss'],
})
export class Tab4Page implements OnInit {
  public fecha = '';
  public idDetalleCarrito = '';

  public idUsuario = '';

  public idProd1 = '';
  public idProd2 = '';
  public cat1 = '';
  public cat2 = '';

  public horaSeleccionada: Date;

  public nombreProducto = '';
  public idCategoria = '';

  public listaCarritoCompra: CarritoCompra[] = [];
  public listaDetalleCarrito: DetalleCarrito[] = [];
  public listaProducto: Producto[] = [];

  constructor(
    private carritoCompraService: CarritoCompraService,
    private detalleCarritoServicioService: DetalleCarritoServicioService
  ) {
    this.horaSeleccionada = new Date();

    this.getCarritoCompraFromBackend();
  }

  public getCarritoCompraFromBackend() {
    this.carritoCompraService.GetAllCarritoCompra().subscribe({
      next: (response: HttpResponse<any>) => {
        this.listaCarritoCompra = response.body;
        console.log(this.listaCarritoCompra);
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

  public getCarritoCompraEdnId(): number {
    var res: number = 0;
    this.carritoCompraService.GetAllCarritoCompraId().subscribe({
      next: (response: HttpResponse<any>) => {
        var auxEndId: CarritoCompra = response.body;
        res = auxEndId.id;

        // res = <CarritoCompra> (response.body[0]).id;
        console.log(
          ' AAAAAAAAAAAAAAAAAAAA' +
            auxEndId.id +
            ' KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK'
        );
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
    return res;
  }

  public addCarritoCompra() {
    if (this.idUsuario == '') {
      alert('no debe dejar el id usuario en blanco');
    } else {
      this.AddCarritoCompraFromBackend(
        this.horaSeleccionada,
        parseInt(this.idUsuario)
      );
    }
  }

  private AddCarritoCompraFromBackend(
    horaSeleccionada: Date,
    idUsuario: number
  ) {
    console.log(this.horaSeleccionada);

    var carritoCompraEntidad = new CarritoCompra();
    carritoCompraEntidad.fecha = this.horaSeleccionada;
    carritoCompraEntidad.idUsuario = idUsuario;

    this.carritoCompraService.AddCarritoCompra(carritoCompraEntidad).subscribe({
      next: (response: HttpResponse<any>) => {
        console.log(response.body);
        if (response.body == 1) {
          alert('Se agrego el USUARIO con exito :)');
          // this.getCarritoCompraFromBackend();//Se actualize el listado
          this.fecha = '';
          this.idUsuario = '';

          //quiero crear un retraso de 1 segundo en esta parte del codigo
          setTimeout(() => {
            this.addDetalleCarritoFrom();
          }, 1000);
        } else {
          alert('Al agregar al USUARIO fallo :(');
        }
      },
      error: (error: any) => {
        console.log(error);
      },
      complete: () => {
        console.log('Fue completado  - this.AddUsuario :)');
      },
    });
  }

  public addDetalleCarritoFrom() {
    // this.addCarritoCompra();

    var idEnd: number = this.getCarritoCompraEdnId();

    setTimeout(() => {
      console.log(' AAAAAAAAAAAAAAAAAAAKKKKKKKKKKKKKKKKKKKKKKKKKKKKK');
      console.log(typeof idEnd);
      console.log(' AAAAAAAAAAAAAAAAAAKKKKKKKKKKKKKKKKKKKKKKKKKKKKK');

      this.addDetalleCarritoFromBackend(
        parseInt(this.cat1),
        parseInt(this.idProd1),
        idEnd
      );
      this.addDetalleCarritoFromBackend(
        parseInt(this.cat2),
        parseInt(this.idProd2),
        idEnd
      );
    }, 5000);
  }

  private addDetalleCarritoFromBackend(
    cantidad: number,
    idProducto: number,
    idCarritoCompra: number
  ) {
    var detalleCarritoEntidad = new DetalleCarrito();
    detalleCarritoEntidad.cantidad = cantidad;
    detalleCarritoEntidad.idProducto = idProducto;
    detalleCarritoEntidad.idCarritoCompra = idCarritoCompra;

    this.detalleCarritoServicioService
      .AddDetalleCarrito(detalleCarritoEntidad)
      .subscribe({
        next: (response: HttpResponse<any>) => {
          console.log(response.body);
          if (response.body == 1) {
            alert('exito :)');
            this.getCarritoCompraFromBackend(); //Se actualize el listado
            this.fecha = '';
            this.idUsuario = '';
          } else {
            alert('mal :(');
          }
        },
        error: (error: any) => {
          console.log(error);
        },
        complete: () => {
          // console.log('Fue completado  - this.AddUsuario :)');
        },
      });
  }

  ngOnInit() {}
}
