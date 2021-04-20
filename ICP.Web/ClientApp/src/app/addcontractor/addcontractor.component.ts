import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import helper from '../helpers/helper';

import { Addcontractor } from '../Models/AddContractor';
import { ContractorService } from '../services/contractor-service';

@Component({
  selector: 'app-addcontractor',
  templateUrl: './addcontractor.component.html',
  styleUrls: ['./addcontractor.component.css']
})
export class AddcontractorComponent {
  public model: Addcontractor;
  private subscription: Subscription;
  mobNumberPattern = "^((\\+91-?)|0)?[0-9]{10}$";

  constructor(private router: Router,
              private service: ContractorService) {
    this.model = new Addcontractor('', '', '');
   }

  onSubmit(){
    this.subscription = this.service
                              .addContractor(this.model)
                              .subscribe(() => {
                                this.goToContractors();
                              },
                              error => console.error(error));
  }

  goToContractors() {
    this.router.navigate(['contractors']);
  }

  ngOnDestroy(): void {
    if(helper.isNullOrUndefined(this.subscription)) {
      this.subscription.unsubscribe();
    }
  }
}

