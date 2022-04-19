import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputCkeditorComponent } from './input-ckeditor/input-ckeditor.component';
import { SafePipe } from './pipe/safe.pipe';
import { MathAbsPipe } from './pipe/math-abs.pipe';

const component = [
  InputCkeditorComponent
]

const pipe = [
  SafePipe,
  MathAbsPipe
]

@NgModule({
  declarations: [
    ...component,
    ...pipe
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ...component,
    ...pipe
  ]
})
export class CustomizeComponentsModule { }
