import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProdutoIncluirComponent } from './produto-incluir.component';

describe('ProdutoIncluirComponent', () => {
  let component: ProdutoIncluirComponent;
  let fixture: ComponentFixture<ProdutoIncluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProdutoIncluirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProdutoIncluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
