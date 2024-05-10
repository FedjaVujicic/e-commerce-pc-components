import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditMonitorsComponent } from './components/edit-monitors/edit-monitors.component';

const routes: Routes = [
  {
    path: "",
    redirectTo: "/edit-monitors",
    pathMatch: "full",
  },
  {
    path: "edit-monitors",
    component: EditMonitorsComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
