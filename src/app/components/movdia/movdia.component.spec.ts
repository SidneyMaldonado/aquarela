import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovdiaComponent } from './movdia.component';

describe('MovdiaComponent', () => {
  let component: MovdiaComponent;
  let fixture: ComponentFixture<MovdiaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MovdiaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MovdiaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
