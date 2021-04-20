import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';
import { PostContract, Contract } from '../Models/AddContract';
import { Contractor } from '../Models/contractor';
import { ContractorService } from '../services/contractor-service';
import helper from '../helpers/helper';

@Component({
  selector: 'app-addcontract',
  templateUrl: './addcontract.component.html',
  styleUrls: ['./addcontract.component.css']
})
export class AddcontractComponent implements OnInit, OnDestroy {
  public contractors: Contractor[];
  public contracts: Contract[];

  private isSuccessful: boolean = false;
  private message: string;
  private cSubscription: Subscription;

  constructor(private service: ContractorService) {

  }

  ngOnInit() {
    this.getAllContracts();
  }

  ngOnDestroy(): void {
    if(helper.isNullOrUndefined(this.cSubscription)) {
      this.cSubscription.unsubscribe();
    }
  }

  onSubmit(model: PostContract) {
    this.message = '';
    this.service.createContract(model)
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

  getAllContracts(): void {
    this.cSubscription = this.service
                              .getAllContracts()
                              .subscribe(res => {
                                this.contracts = res;
                              },
                              error => console.error(error));
  }
}
