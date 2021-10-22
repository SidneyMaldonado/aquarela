import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FornecedorIncluirComponent } from './fornecedor-incluir.component';

describe('FornecedorIncluirComponent', () => {
  let component: FornecedorIncluirComponent;
  let fixture: ComponentFixture<FornecedorIncluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FornecedorIncluirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FornecedorIncluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
