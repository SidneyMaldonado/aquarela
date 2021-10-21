import { TestBed } from '@angular/core/testing';

import { GrupoprodutoService } from './grupoproduto.service';

describe('GrupoprodutoService', () => {
  let service: GrupoprodutoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GrupoprodutoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
