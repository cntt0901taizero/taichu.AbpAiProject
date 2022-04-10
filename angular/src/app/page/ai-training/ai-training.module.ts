import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AiTrainingRoutingModule } from './ai-training-routing.module';
import { AiTrainingComponent } from './ai-training.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgZorroAntdModule } from 'src/app/ng-zorro-antd.module';


@NgModule({
  declarations: [
    AiTrainingComponent
  ],
  imports: [
    SharedModule,
    NgZorroAntdModule,
    CommonModule,
    AiTrainingRoutingModule,
  ]
})
export class AiTrainingModule { }
