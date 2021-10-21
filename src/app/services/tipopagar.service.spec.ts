import { TestBed } from '@angular/core/testing';

import { TipopagarService } from './tipopagar.service';

describe('TipopagarService', () => {
  let service: TipopagarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TipopagarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
