import { TestBed } from '@angular/core/testing';

import { ParelareceberService } from './parelareceber.service';

describe('ParelareceberService', () => {
  let service: ParelareceberService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ParelareceberService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
