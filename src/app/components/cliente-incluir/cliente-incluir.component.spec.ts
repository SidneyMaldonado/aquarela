import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClienteIncluirComponent } from './cliente-incluir.component';

describe('ClienteIncluirComponent', () => {
  let component: ClienteIncluirComponent;
  let fixture: ComponentFixture<ClienteIncluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClienteIncluirComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClienteIncluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
