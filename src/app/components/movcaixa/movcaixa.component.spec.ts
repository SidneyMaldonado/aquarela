import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovcaixaComponent } from './movcaixa.component';

describe('MovcaixaComponent', () => {
  let component: MovcaixaComponent;
  let fixture: ComponentFixture<MovcaixaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MovcaixaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MovcaixaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
