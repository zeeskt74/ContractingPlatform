import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
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
  public model: AddContract;

  private isSuccessful: boolean;
  private message: string;
  private subscription: Subscription;
  private cSubscription: Subscription;

  constructor(private service: ContractorService) {
    this.model = new AddContract(0,0);
  }

  ngOnInit() {
    this.getAllContractors();
    this.getAllContracts();
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
    this.message = '';
    this.service.createContract(this.model)
                .subscribe(
                  () => {
                    this.getAllContracts();
                    this.isSuccessful = true;
                    this.message = "Saved successfully."
                  },
                  errorRes => {
                    this.isSuccessful = false;
                    this.message = errorRes.error.text;
                  });
  }


  getAllContractors(): void {
    this.subscription = this.service
                              .getAllContractors()
                              .subscribe((res) => {
                                this.contractors = res;
                              },
                              error => console.error(error));
  }

  getAllContracts(): void {
    this.cSubscription = this.service
                              .getAllContracts()
                              .subscribe(res => {
                                this.contracts = res;
                              },
                              error => console.error(error));
  }
}
