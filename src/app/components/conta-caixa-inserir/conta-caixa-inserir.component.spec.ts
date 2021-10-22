import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContaCaixaInserirComponent } from './conta-caixa-inserir.component';

describe('ContaCaixaInserirComponent', () => {
  let component: ContaCaixaInserirComponent;
  let fixture: ComponentFixture<ContaCaixaInserirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContaCaixaInserirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContaCaixaInserirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
