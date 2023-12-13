import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef as MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent implements OnInit {
  message: string;
  title: string = 'Alert';
  status: string;


  public constructor(
    public readonly dialogRef: MatDialogRef<AlertComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AlertDialog) {
    this.message = data.message;
    this.title = data.title;
    this.status = data.status;
  }

  public ngOnInit() {
  }
}


export class AlertDialog {
  public message: string;
  public title: string;
  public status: string;
}