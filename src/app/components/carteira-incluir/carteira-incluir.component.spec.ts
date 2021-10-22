import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarteiraIncluirComponent } from './carteira-incluir.component';

describe('CarteiraIncluirComponent', () => {
  let component: CarteiraIncluirComponent;
  let fixture: ComponentFixture<CarteiraIncluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarteiraIncluirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarteiraIncluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
