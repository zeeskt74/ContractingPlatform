import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';

import { ContractorDetail } from '../Models/contractor';
import { ContractorService } from '../services/contractor-service';

@Component({
  selector: 'app-contractors',
  templateUrl: './contractors.component.html',
  styleUrls: ['./contractors.component.css']
})
export class ContractorsComponent implements OnInit, OnDestroy  {
  public contructors: ContractorDetail[];
  private subscription: Subscription;

  constructor(private service: ContractorService) {
  }

  ngOnInit(): void {
    this.subscription = this.service.getAllContractors()
                              .subscribe(result => {
                                      this.contructors = result;
                              },
                              error => console.error(error));
  }

  ngOnDestroy(): void {
    if(this.subscription !== null) {
      this.subscription.unsubscribe();
    }
  }
}
