import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagarIncluirComponent } from './pagar-incluir.component';

describe('PagarIncluirComponent', () => {
  let component: PagarIncluirComponent;
  let fixture: ComponentFixture<PagarIncluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PagarIncluirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PagarIncluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
