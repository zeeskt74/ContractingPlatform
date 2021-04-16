import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';
import { AddContract, Contract } from '../Models/AddContract';
import { Contractor } from '../Models/contractor';
import { ContractorService } from '../services/contractor-service';

@Component({
  selector: 'app-addcontract',
  templateUrl: './addcontract.component.html',
  styleUrls: ['./addcontract.component.css']
})
export class AddcontractComponent implements OnInit, OnDestroy {
  public contractors: Contractor[];
  public contracts: Contract[];
  public model: AddContract = new AddContract(0,0);

  private subscription: Subscription;
  private cSubscription: Subscription;

  constructor(private service: ContractorService) { }


  ngOnInit() {
    this.subscription = this.service
                              .getAllContractors()
                              .subscribe((res) => {
                                this.contractors = res;
                              },
                              error => console.error(error));

    this.cSubscription = this.service
                              .getContracts(this.model.mainContractorId)
                              .subscribe(res =>{
                                this.contracts = res;
                              },
                              error => console.error(error));
  }

  ngOnDestroy(): void {
    if(this.subscription !== null) {
      this.subscription.unsubscribe();
    }
    if(this.cSubscription !== null) {
      this.cSubscription.unsubscribe();
    }
  }

  onSubmit() {
    this.service.createContract(this.model).subscribe(null, error => console.error(error));
  }

}
