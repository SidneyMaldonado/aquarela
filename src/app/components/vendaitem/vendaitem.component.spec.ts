import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VendaitemComponent } from './vendaitem.component';

describe('VendaitemComponent', () => {
  let component: VendaitemComponent;
  let fixture: ComponentFixture<VendaitemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VendaitemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VendaitemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
