import { TestBed } from '@angular/core/testing';

import { GrupoProdutoService } from './grupoproduto.service';

describe('GrupoprodutoService', () => {
  let service: GrupoProdutoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GrupoProdutoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
