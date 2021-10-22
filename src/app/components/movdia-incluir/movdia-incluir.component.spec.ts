import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovdiaIncluirComponent } from './movdia-incluir.component';

describe('MovdiaIncluirComponent', () => {
  let component: MovdiaIncluirComponent;
  let fixture: ComponentFixture<MovdiaIncluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MovdiaIncluirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MovdiaIncluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
