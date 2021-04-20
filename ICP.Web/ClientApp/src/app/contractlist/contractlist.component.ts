import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';
import helper from '../helpers/helper';
import { PostContract } from '../Models/AddContract';
import { Contractor } from '../Models/contractor';
import { ContractorService } from '../services/contractor-service';

@Component({
  selector: 'app-contractlist',
  templateUrl: './contractlist.component.html',
  styleUrls: ['./contractlist.component.css']
})
export class ContractlistComponent implements OnInit, OnDestroy {
  @Output() submit = new EventEmitter<PostContract>();

  public contractors: Contractor[];

  private model: PostContract;
  private subscription: Subscription;

  constructor(private service: ContractorService) {
    this.model = new PostContract(0,0);
  }

  ngOnInit() {
    this.getAllContractors();
  }

  ngOnDestroy(): void {
    if(helper.isNullOrUndefined(this.subscription)) {
      this.subscription.unsubscribe();
    }
  }

  getAllContractors(): void {
    this.subscription = this.service
                              .getAllContractors()
                              .subscribe((res) => {
                                this.contractors = res;
                              },
                              error => console.error(error));
  }

  onSubmit() {
    this.submit.emit(this.model);
  }
}
