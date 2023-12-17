import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// used to create fake backend
import { fakeBackendProvider } from './_helpers';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HomeComponent } from './home';
import { AdminComponent } from './admin';
import { LoginComponent } from './login';
import { SingleEntryComponent } from './feature_components/single-entry';
import { MultipleEntryComponent } from './feature_components/multiple-entry';
import { ReviewComponent } from './feature_components/review';
import { RegistrationComponent } from './registration';
import { UserComponent } from './feature_components/user';
import { AddUserComponent } from './feature_components/add-user';
import { AlertComponent } from './alert/alert.component';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DEFAULT_OPTIONS, MatDialogModule } from '@angular/material/dialog';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table'  
import { FlexLayoutModule } from '@angular/flex-layout';
import { MasterComponent } from './master';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatListModule } from '@angular/material/list';
import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatMenuModule } from '@angular/material/menu';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgTiltModule } from '@geometricpanda/angular-tilt';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        BrowserAnimationsModule,
        MatCardModule,
        MatTabsModule,
        MatTableModule,
        AppRoutingModule,
        MatSelectModule,
        MatButtonModule,
        MatDialogModule,
        MatPaginatorModule,
        FlexLayoutModule,
        MatInputModule,
        MatIconModule,
        MatMenuModule,
        MatSortModule,
        MatProgressSpinnerModule,
        NgTiltModule
    ],
    declarations: [
        AppComponent,
        LoginComponent,
        RegistrationComponent,
        HomeComponent,
        AdminComponent,
        SingleEntryComponent,
        MultipleEntryComponent,
        ReviewComponent,
        AlertComponent,
        UserComponent,
        MasterComponent,
        AddUserComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        {provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: false}}

        // // provider used to create fake backend
        // fakeBackendProvider
    ],
    bootstrap: [AppComponent],
    entryComponents: [
        AddUserComponent
      ]
})

export class AppModule { }