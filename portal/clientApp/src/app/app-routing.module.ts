import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthGuard } from './auth.guard';
import { MainComponent } from './main/main.component';
import { RegisterComponent } from './register/register.component';
import { ReviewComponent } from './review/review.component';
import { SingninComponent } from './singnin/singnin.component';
import { UploadComponent } from './upload/upload.component';

const routes: Routes = [
  { path:'sign-in', component: SingninComponent },
  { path:'register', component:RegisterComponent },
  { path:  'upload', component: UploadComponent, canActivate: [AuthGuard] },
  { path:'review', component: ReviewComponent, canActivate: [AuthGuard] }
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
