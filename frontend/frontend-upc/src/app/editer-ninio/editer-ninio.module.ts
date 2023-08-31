import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditerNinioPageRoutingModule } from './editer-ninio-routing.module';

import { EditerNinioPage } from './editer-ninio.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditerNinioPageRoutingModule
  ],
  declarations: [EditerNinioPage]
})
export class EditerNinioPageModule {}
