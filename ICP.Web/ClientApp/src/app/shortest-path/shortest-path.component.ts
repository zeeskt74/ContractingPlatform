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

  constructor(private service: ContractorService) { }

  ngOnInit() {
  }

  onShortestPath(model: PostContract) {
    this.service.getShortestPath(model)
                .subscribe(
                  (res) => {
                    this.paths = res;
                  },
                  error => console.error(error));
  }
}
