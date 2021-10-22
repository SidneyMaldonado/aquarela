import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoPagarIncluirComponent } from './tipo-pagar-incluir.component';

describe('TipoPagarIncluirComponent', () => {
  let component: TipoPagarIncluirComponent;
  let fixture: ComponentFixture<TipoPagarIncluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TipoPagarIncluirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TipoPagarIncluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
