import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditMonitorsComponent } from './components/edit-products/edit-monitors/edit-monitors.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { EditGpusComponent } from './components/edit-products/edit-gpus/edit-gpus.component';

const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
  },
  {
    path: "login",
    component: LoginComponent,
  },
  {
    path: "register",
    component: RegisterComponent,
  },
  {
    path: "edit-monitors",
    component: EditMonitorsComponent,
  },
  {
    path: "edit-gpus",
    component: EditGpusComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
