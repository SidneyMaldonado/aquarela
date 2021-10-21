import { TestBed } from '@angular/core/testing';

import { SaldoatualizadoService } from './saldoatualizado.service';

describe('SaldoatualizadoService', () => {
  let service: SaldoatualizadoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SaldoatualizadoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
