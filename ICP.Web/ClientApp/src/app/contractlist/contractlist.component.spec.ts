import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractlistComponent } from './contractlist.component';

describe('ContractlistComponent', () => {
  let component: ContractlistComponent;
  let fixture: ComponentFixture<ContractlistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
