import { TestBed } from '@angular/core/testing';

import { ContaCaixaService } from './contacaixa.service';

describe('ContacaixaService', () => {
  let service: ContaCaixaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContaCaixaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

});
