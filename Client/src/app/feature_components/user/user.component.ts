import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { Clipboard } from '@angular/cdk/clipboard';
import {  PageEvent } from '@angular/material/paginator';
//import { AlertsService } from '@app/_services/alerts.service';
import { DataDtoRange, DataDtoFilter } from '@app/_services/dto/response.dto';
import { MasterService } from '@app/_services/master.service';
import { AddUserComponent } from '../add-user';

//@Component({ templateUrl: 'user.component.html' })
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  userColumns: string[] = ['firstName', 'lastName', 'email', 'role'];
  userresult: IUsers[] = [];
  checklistId: number;
  userData:any;
  discription:string[];
  selectedStatusName:string;
  public range: DataDtoRange = new DataDtoRange();
  public filter: DataDtoFilter = new DataDtoFilter();
  statusName: any = [{id:0,name:''}];
  pdialogConfig: MatDialogConfig;
  dialogWithForm: MatDialogRef<AddUserComponent>;
 
  constructor(private dialogModel: MatDialog,
    private clipboard: Clipboard,
    //public readonly alertsService: AlertsService,
    public readonly masterService:MasterService,

   //@Inject(MAT_DIALOG_DATA) public data: any
  ) {
    
    this.checklistId =0;// data;
    this.range.limit = 10;

  }

  public async ngOnInit(): Promise<void> {
    this.loadUsers();
  }

  public async loadUsers() {
    debugger;
    this.userData = this.masterService.getAllUsers().then(response => {
      if (response.isSuccess) {
        this.userresult = response.data;
        this.range.total = response.dataTotalCount;
      }
    });
  } 

  public async onPage(event: PageEvent) {
    this.range.limit = event.pageSize;
    this.range.offset = event.pageIndex * event.pageSize;
    await this.loadUsers();
  }

  getstatusName(statusId:number) {
    var data = this.statusName.filter((c: { id: number; }) => c.id == statusId);
    if(data.length > 0){
      return data[0].name;
    }
    return null;
  }
  
  simpleDialog: MatDialogRef<AddUserComponent>;

  addUserDialog() {

    let dialogRef = this.dialogModel.open(AddUserComponent, {
      //panelClass: 'fullscreen-dialog',
        width: '50%',
    });
    
    // When user close the dialog
    dialogRef.afterClosed().subscribe(result => {
      debugger;
      console.log('You have closed the dialog');
      if (result) {
        this.loadUsers();
      }
    });

    }

  }

  export interface IUsers {
    email: string
    emailConfirmed: boolean
    firstName: string
    id: string
    isActive: boolean
    lastName: string
    role: string
  }
