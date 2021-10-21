import { TestBed } from '@angular/core/testing';

import { VendaitemService } from './vendaitem.service';

describe('VendaitemService', () => {
  let service: VendaitemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VendaitemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
