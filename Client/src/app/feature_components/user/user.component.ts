import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomvalidationService } from '@app/_services/custom-validation.service';
import { Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Clipboard } from '@angular/cdk/clipboard';
import {  PageEvent } from '@angular/material/paginator';
//import { AlertsService } from '@app/_services/alerts.service';
import { DataDtoRange, DataDtoFilter } from '@app/_services/dto/response.dto';
import { MasterService } from '@app/_services/master.service';

//@Component({ templateUrl: 'user.component.html' })
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  historyColumns: string[] = ['date', 'description', 'status', 'user'];
  historyresult: IHistory[] = [];
  checklistId: number;
  historyData:any;
  discription:string[];
  selectedStatusName:string;
  public range: DataDtoRange = new DataDtoRange();
  public filter: DataDtoFilter = new DataDtoFilter();
  statusName: any = [{id:0,name:'Not reviewed'},{id:1,name:'Not ready'},{id:2,name:'Minimally ready'},
    {id:3,name:'Partially ready'},{id:4,name:'Mostly ready'},{id:5,name:'Completely ready'}];
  public constructor(
    private clipboard: Clipboard,
    //public readonly alertsService: AlertsService,
    public readonly masterService:MasterService,

   //@Inject(MAT_DIALOG_DATA) public data: any
  ) {
    
    this.checklistId =0;// data;
    this.range.limit = 10;

  }

  public async ngOnInit(): Promise<void> {
    this.loadHistory();
  }

  public async loadHistory() {
    this.historyData = this.masterService.getAllUsers().then(r => {
      if (r.success) {
        this.historyresult = r.data;
        this.range.total = r.dataTotalCount;
      }
    });
  } 

  public async onPage(event: PageEvent) {
    this.range.limit = event.pageSize;
    this.range.offset = event.pageIndex * event.pageSize;
    await this.loadHistory();
  }

  getstatusName(statusId:number) {
    var data = this.statusName.filter((c: { id: number; }) => c.id == statusId);
    if(data.length > 0){
      return data[0].name;
    }
    return null;
  } 

  }

  export interface IHistory {
    createdDate: Date;
    discription : string;
    status: string;
    userName:string;
  }
