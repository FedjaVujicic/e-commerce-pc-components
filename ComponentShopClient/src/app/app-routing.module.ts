import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MonitorsComponent } from './components/products/monitors/monitors.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { GpusComponent } from './components/products/gpus/gpus.component';
import { GpuInfoComponent } from './components/products/gpu-info/gpu-info.component';
import { MonitorInfoComponent } from './components/products/monitor-info/monitor-info.component';

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
    path: "monitors",
    component: MonitorsComponent,
  },
  {
    path: "gpus",
    component: GpusComponent,
  },
  {
    path: "gpu-info",
    component: GpuInfoComponent,
  },
  {
    path: "monitor-info",
    component: MonitorInfoComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
