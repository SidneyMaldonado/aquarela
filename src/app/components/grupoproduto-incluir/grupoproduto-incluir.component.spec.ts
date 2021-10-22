import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrupoprodutoIncluirComponent } from './grupoproduto-incluir.component';

describe('GrupoprodutoIncluirComponent', () => {
  let component: GrupoprodutoIncluirComponent;
  let fixture: ComponentFixture<GrupoprodutoIncluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrupoprodutoIncluirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrupoprodutoIncluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
