import { Inject, Injectable } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog as MatDialog } from '@angular/material/dialog';
import { MatSnackBar as MatSnackBar } from '@angular/material/snack-bar';
import { AlertComponent, AlertDialog } from '@app/alert/alert.component';

@Injectable({
  providedIn: 'root'
})
export class AlertsService {
  private readonly ERROR_GENERIC_MESSAGE = `Oops something went wrong!`;
  dialogTitle: any;

  public constructor(private readonly dialog: MatDialog,
    //private snackBar: MatSnackBar
    ){

    }   
    
  public showError(message: string = null) {
    this.dialog.open(AlertComponent, { data: message || this.ERROR_GENERIC_MESSAGE });
  }

  public showInfo(message: string, title: string = "Message",status : string = ""): Promise<void> {
    let alterDialog = new AlertDialog();
    alterDialog.message = message;
    alterDialog.title = title;
    alterDialog.status = status;
    
    return new Promise((resolve) => {
      let dialogRef = this.dialog.open(AlertComponent, {
        data: alterDialog, 
        panelClass: 'dialog-custom',
        autoFocus: false,     
      });
      
      dialogRef.afterClosed().subscribe(() => {
        resolve();
      });
    });
  }

  public showOnce(message: string): Promise<void> {
    if (!message || localStorage.getItem(btoa(message))) {
      return new Promise((resolve, _) => { resolve(); });
    }

    localStorage.setItem(btoa(message), "1");

    return new Promise((resolve) => {
      let dialogRef = this.dialog.open(AlertComponent, { data: message });
      dialogRef.afterClosed().subscribe(() => {
        resolve();
      });
    });
  }

  public showSuccessNotification(message: string) {
    // this.snackBar.open(message, null, {
    //   duration: 3000,
    //   verticalPosition: 'bottom',
    //   panelClass: 'success-notification-panel',
    // })
  }
}
