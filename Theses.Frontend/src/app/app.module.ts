import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ThesesListComponent } from './components/theses/theses-list/theses-list.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: ThesesListComponent
  },
  {
    path: 'theses',
    component: ThesesListComponent
  }
]; // sets up routes constant where you define your routes

@NgModule({
  declarations: [
    AppComponent,
    ThesesListComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
export class AppRoutingModule { }
