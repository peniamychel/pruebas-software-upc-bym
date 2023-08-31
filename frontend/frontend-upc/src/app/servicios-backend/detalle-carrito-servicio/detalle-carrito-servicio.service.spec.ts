import { TestBed } from '@angular/core/testing';

import { DetalleCarritoServicioService } from './detalle-carrito-servicio.service';

describe('DetalleCarritoServicioService', () => {
  let service: DetalleCarritoServicioService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DetalleCarritoServicioService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
