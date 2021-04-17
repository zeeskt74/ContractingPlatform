import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { environment } from "src/environments/environment";
import { AddContract, Contract } from "../Models/AddContract";
import { Addcontractor } from "../Models/AddContractor";
import { ContractorDetail } from "../Models/contractor";

@Injectable({
  providedIn: 'root',
 })
export class ContractorService {

  constructor(private http: HttpClient) {
  }

  getAllContractors(): Observable<ContractorDetail[]> {
    return this.http.get<ContractorDetail[]>(environment.apiUrl + 'Contractors')
  }

  addContractor(contractor: Addcontractor): Observable<any> {
    return this.http.post(environment.apiUrl + "Contractors", contractor);
  }

  createContract(addContract: AddContract): Observable<any> {
    let params = new HttpParams()
                  .set('mainContractorId', addContract.mainContractorId.toString())
                  .set('relationContractorId', addContract.relationContractorId.toString());

    return this.http.post(environment.apiUrl + "Contract", params);
  }

  getContracts(mainContractorId: number): Observable<any> {
    return this.http.get<Contract[]>(environment.apiUrl + "Contract",
                                    {
                                      params: {
                                        mainContractorId: mainContractorId.toString()
                                      }
                                    });
  }

  getAllContracts(): Observable<Contract[]> {
    return this.http.get<Contract[]>(environment.apiUrl + "Contract/all");
  }
}
