import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContacaixaComponent } from './contacaixa.component';

describe('ContacaixaComponent', () => {
  let component: ContacaixaComponent;
  let fixture: ComponentFixture<ContacaixaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContacaixaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContacaixaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
