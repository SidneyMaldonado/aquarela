import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParcelaReceberComponent } from './parcelareceber.component';

describe('ParcelaReceberComponent', () => {
  let component: ParcelaReceberComponent;
  let fixture: ComponentFixture<ParcelaReceberComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ParcelaReceberComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ParcelaReceberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
