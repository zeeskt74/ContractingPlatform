import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ContractorsComponent } from './contractors/contractors.component';
import { AddcontractorComponent } from './addcontractor/addcontractor.component';
import { AddcontractComponent } from './addcontract/addcontract.component';
import { ShortestPathComponent } from './shortest-path/shortest-path.component';
import { ContractlistComponent } from './contractlist/contractlist.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    //HomeComponent,
    ContractorsComponent,
    AddcontractorComponent,
    AddcontractComponent,
    ShortestPathComponent,
    ContractlistComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: ContractorsComponent, pathMatch: 'full' },
      { path: 'contractors', component: ContractorsComponent },
      { path: 'addcontractor', component: AddcontractorComponent },
      { path: 'addcontract', component: AddcontractComponent },
      { path: 'shortest-path', component: ShortestPathComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
