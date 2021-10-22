import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaldoatualizadoIncluirComponent } from './saldoatualizado-incluir.component';

describe('SaldoatualizadoIncluirComponent', () => {
  let component: SaldoatualizadoIncluirComponent;
  let fixture: ComponentFixture<SaldoatualizadoIncluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SaldoatualizadoIncluirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SaldoatualizadoIncluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
