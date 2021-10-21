import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParcelapagarComponent } from './parcelapagar.component';

describe('ParcelapagarComponent', () => {
  let component: ParcelapagarComponent;
  let fixture: ComponentFixture<ParcelapagarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ParcelapagarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ParcelapagarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
