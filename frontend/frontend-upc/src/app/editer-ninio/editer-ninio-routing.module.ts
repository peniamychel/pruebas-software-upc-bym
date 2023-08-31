import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditerNinioPage } from './editer-ninio.page';

const routes: Routes = [
  {
    path: '',
    component: EditerNinioPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditerNinioPageRoutingModule {}
