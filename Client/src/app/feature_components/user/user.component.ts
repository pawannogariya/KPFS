import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { Clipboard } from '@angular/cdk/clipboard';
import {  MatPaginator, PageEvent } from '@angular/material/paginator';
//import { AlertsService } from '@app/_services/alerts.service';
import { DataDtoRange, DataDtoFilter } from '@app/_services/dto/response.dto';
import { MasterService } from '@app/_services/master.service';
import { AddUserComponent } from '../add-user';
import { MatTableDataSource } from '@angular/material/table';

//@Component({ templateUrl: 'user.component.html' })
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit, AfterViewInit {

  userColumns: string[] = ['firstName', 'lastName', 'email', 'role'];
  userresult: MatTableDataSource<IUsers>;
  checklistId: number;
  userData:any;
  discription:string[];
  selectedStatusName:string;
  public range: DataDtoRange = new DataDtoRange();
  public filter: DataDtoFilter = new DataDtoFilter();
  statusName: any = [{id:0,name:''}];
  pdialogConfig: MatDialogConfig;
  dialogWithForm: MatDialogRef<AddUserComponent>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  constructor(private dialogModel: MatDialog,
    private clipboard: Clipboard,
    //public readonly alertsService: AlertsService,
    public readonly masterService:MasterService,

   //@Inject(MAT_DIALOG_DATA) public data: any
  ) {
    
    this.checklistId =0;// data;
    this.range.limit = 10;
    //this.userresult = new MatTableDataSource(this.usersData);
  }

  public async ngOnInit(): Promise<void> {
    this.loadUsers();
  }

  ngAfterViewInit(): void {
    if(this.userresult)
      this.userresult.paginator = this.paginator;
  }

  public async loadUsers() {
    // this.userresult = new MatTableDataSource(this.getStaticUserData());
    // return;
      this.userData = this.masterService.getAllUsers().then(response => {
      if (response.isSuccess) {
        //this.userresult = response.data;
        this.userresult = new MatTableDataSource(response.data);
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
      console.log('You have closed the dialog');
      if (result) {
        this.loadUsers();
      }
    });

    }

    applyFilter(event: Event) {
      const filterValue = (event.target as HTMLInputElement).value;
      this.userresult.filter = filterValue.trim().toLowerCase();
  
      if (this.userresult.paginator) {
        this.userresult.paginator.firstPage();
      }
    }

    getStaticUserData()
    {
      return [
    {
      "id": "21e83156-e8fb-4e54-b5d7-89fad9d6597e",
      "email": "badi4net3@gmail.com",
      "firstName": "User",
      "lastName": "User",
      "isActive": true,
      "role": "User",
      "emailConfirmed": false
    },
    {
        "id": "5c1d97d9-9baa-472a-8eec-2ff9271e12bd",
        "email": "badi4net2@gmail.com",
        "firstName": "Sarfaraz",
        "lastName": "Badi",
        "isActive": true,
        "role": "Reviewer",
        "emailConfirmed": true
    },
    {
      "id": "668f96df-8e03-11ee-b507-e86a64b47aae",
      "email": "badi4net@gmail.com",
      "firstName": "Super",
      "lastName": "Admin",
      "isActive": true,
      "role": "Admin",
      "emailConfirmed": true
    },
    {
        "id": "8b322f95-3fb2-4d36-ad2d-aa3dbcbd1d32",
        "email": "badi4net1@gmail.com",
        "firstName": "Sarfaraz",
        "lastName": "Badi",
        "isActive": true,
        "role": "User",
        "emailConfirmed": false
    },
    {
      "id": "668f96df-8e03-11ee-b507-e86a64b47aae",
      "email": "badi4net@gmail.com",
      "firstName": "Super",
      "lastName": "Admin",
      "isActive": true,
      "role": "Admin",
      "emailConfirmed": true
    },
    {
        "id": "8b322f95-3fb2-4d36-ad2d-aa3dbcbd1d32",
        "email": "badi4net1@gmail.com",
        "firstName": "Sarfaraz",
        "lastName": "Badi",
        "isActive": true,
        "role": "User",
        "emailConfirmed": false
    }
  ]
 
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
