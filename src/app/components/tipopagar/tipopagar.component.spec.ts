import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TipopagarComponent } from './tipopagar.component';

describe('TipopagarComponent', () => {
  let component: TipopagarComponent;
  let fixture: ComponentFixture<TipopagarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TipopagarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TipopagarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
