import { environment } from './../../environments/environment';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { Tab5Page } from './tab5.page';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { Modal1Page } from '../modal/modal1/modal1.page';
import { Modal1PageModule } from '../modal/modal1/modal1.module';

const routes: Routes = [
  {
    path: '',
    component: Tab5Page
  }
];

@NgModule({
  imports: [
    CommonModule,
    IonicModule,
    RouterModule.forChild(routes),
    Modal1PageModule
  ],
  exports: [RouterModule],
})
export class Tab5PageRoutingModule {}
