import { TestBed } from '@angular/core/testing';

import { MovdiaService } from './movdia.service';

describe('MovdiaService', () => {
  let service: MovdiaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MovdiaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
