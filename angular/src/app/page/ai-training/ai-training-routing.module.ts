import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AiTrainingComponent } from './ai-training.component';

const routes: Routes = [
  {path: '', component: AiTrainingComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AiTrainingRoutingModule { }
