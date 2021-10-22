import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParcelapagarIncluirComponent } from './parcelapagar-incluir.component';

describe('ParcelapagarIncluirComponent', () => {
  let component: ParcelapagarIncluirComponent;
  let fixture: ComponentFixture<ParcelapagarIncluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ParcelapagarIncluirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ParcelapagarIncluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
