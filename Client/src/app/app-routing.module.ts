import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { AdminComponent } from './admin';
import { LoginComponent } from './login';
import { AuthGuard } from './_helpers';
import { Role } from './_models';
import { SingleEntryComponent } from './feature_components/single-entry';
import { MultipleEntryComponent } from './feature_components/multiple-entry';
import { ReviewComponent } from './feature_components/review';
import { RegistrationComponent } from './registration';
import { UserComponent } from './feature_components/user';
import { MasterComponent } from './master';
import { ConfirmationEmailComponent } from './confirmation-email';

const routes: Routes = [
    {
        path: 'home',
        component: HomeComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'admin',
        component: AdminComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.Admin] }
    },
    {
        path: 'single-entry',
        component: SingleEntryComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.Admin,Role.User] }
    },
    {
        path: 'multiple-entry',
        component: MultipleEntryComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.Admin,Role.User] }
    },
    {
        path: 'review',
        component: ReviewComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.Admin] }
    },
    {
        path: 'user',
        component: UserComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.Admin] }
    },
    {
        path: 'master',
        component: MasterComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.Admin] }
    },
    {
        path: 'confirm-email/:token/:email',
        component: ConfirmationEmailComponent,
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'registration',
        component: RegistrationComponent
    },
    {
        path: '',
        component: LoginComponent
    },
    // otherwise redirect to home
    { path: '**', redirectTo: 'login' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
