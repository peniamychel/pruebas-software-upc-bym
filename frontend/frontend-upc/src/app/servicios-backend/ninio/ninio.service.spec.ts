import { TestBed } from '@angular/core/testing';

import { NinioService } from './ninio.service';

describe('NinioService', () => {
  let service: NinioService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NinioService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
