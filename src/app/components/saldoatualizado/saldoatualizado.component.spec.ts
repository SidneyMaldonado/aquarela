import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaldoatualizadoComponent } from './saldoatualizado.component';

describe('SaldoatualizadoComponent', () => {
  let component: SaldoatualizadoComponent;
  let fixture: ComponentFixture<SaldoatualizadoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SaldoatualizadoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SaldoatualizadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
