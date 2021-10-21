import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrupoprodutoComponent } from './grupoproduto.component';

describe('GrupoprodutoComponent', () => {
  let component: GrupoprodutoComponent;
  let fixture: ComponentFixture<GrupoprodutoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrupoprodutoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrupoprodutoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
