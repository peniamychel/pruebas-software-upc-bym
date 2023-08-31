import { TestBed } from '@angular/core/testing';

import { DejaEnGuarderiaService } from './deja-en-guarderia.service';

describe('DejaEnGuarderiaService', () => {
  let service: DejaEnGuarderiaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DejaEnGuarderiaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
