import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { EditMonitorsComponent } from './components/edit-products/edit-monitors/edit-monitors.component';
import { EditMonitorsFormComponent } from './components/edit-products/edit-monitors/edit-monitors-form/edit-monitors-form.component';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { EditGpusComponent } from './components/edit-products/edit-gpus/edit-gpus.component';
import { EditGpusFormComponent } from './components/edit-products/edit-gpus/edit-gpus-form/edit-gpus-form.component';

@NgModule({
  declarations: [
    AppComponent,
    EditMonitorsComponent,
    EditMonitorsFormComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    EditGpusComponent,
    EditGpusFormComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
