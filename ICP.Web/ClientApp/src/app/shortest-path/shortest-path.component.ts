import { Component, OnInit } from '@angular/core';
import { Contract, PostContract } from '../Models/AddContract';
import { ContractorService } from '../services/contractor-service';

@Component({
  selector: 'app-shortest-path',
  templateUrl: './shortest-path.component.html',
  styleUrls: ['./shortest-path.component.css']
})
export class ShortestPathComponent implements OnInit {
  public paths: number[];

  private message: string;

  constructor(private service: ContractorService) { }

  ngOnInit() {
  }

  onShortestPath(model: PostContract) {
    this.message = '';
    this.service.getShortestPath(model)
                .subscribe(
                  (res) => {
                    this.paths = res;
                  },
                  (errorRes) => {
                    this.message = errorRes.error;
                    console.error(errorRes);
                  });
  }
}
