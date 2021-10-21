import { TestBed } from '@angular/core/testing';

import { MovcaixaService } from './movcaixa.service';

describe('MovcaixaService', () => {
  let service: MovcaixaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MovcaixaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
