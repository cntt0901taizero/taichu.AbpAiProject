import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AiTrainingRoutingModule } from './ai-training-routing.module';
import { AiTrainingComponent } from './ai-training.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgZorroAntdModule } from 'src/app/ng-zorro-antd.module';
import { UpsertDatatrainingComponent } from './components/upsert-datatraining/upsert-datatraining.component';


@NgModule({
  declarations: [
    AiTrainingComponent,
    UpsertDatatrainingComponent
  ],
  imports: [
    SharedModule,
    NgZorroAntdModule,
    CommonModule,
    AiTrainingRoutingModule,
  ]
})
export class AiTrainingModule { }
