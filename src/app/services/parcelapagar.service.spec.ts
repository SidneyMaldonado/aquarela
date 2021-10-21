import { TestBed } from '@angular/core/testing';

import { ParcelapagarService } from './parcelapagar.service';

describe('ParcelapagarService', () => {
  let service: ParcelapagarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ParcelapagarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
