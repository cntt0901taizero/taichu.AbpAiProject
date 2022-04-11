import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AiTrainingService } from '@proxy/ai-training';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { NzNotificationService } from 'ng-zorro-antd/notification';

@Component({
  selector: 'app-upsert-datatraining',
  templateUrl: './upsert-datatraining.component.html',
  styleUrls: ['./upsert-datatraining.component.scss']
})
export class UpsertDatatrainingComponent implements OnInit {
  @Input() dataItem: any;
  rfDataModal: FormGroup;
  saving = false;
  
  constructor(
    private _aiTrainingService: AiTrainingService,
    private modal: NzModalRef,
    private fb: FormBuilder,
    private notification: NzNotificationService
  ) { }

  ngOnInit(): void {
    this.rfDataModal = this.fb.group({
      id: 0,
      inputString: [''],
      outputString: [''],
      funcName: [''],
      link: [''],
      note: [''],
    });
    if (this.dataItem) {
      this.rfDataModal.patchValue(this.dataItem);
    }
  }

  save(): void {
    if (this.rfDataModal.invalid) {
      this.notification.error('Vui lòng xem lại thông tin form', '', { nzPlacement: 'bottomRight' });
        for (const i in this.rfDataModal.controls) {
            this.rfDataModal.controls[i].markAsDirty();
            this.rfDataModal.controls[i].updateValueAndValidity();
        }
    } else {
        this.saving = true;
        if (this.rfDataModal.get('id').value > 0) {
          this._aiTrainingService.update(this.rfDataModal.get('id').value, this.rfDataModal.value).subscribe(result => {
              this.notification.success('Thành công', '', { nzPlacement: 'bottomRight' });
              this.close(true);
              localStorage.removeItem('data-training');
            });
        } else {
          this._aiTrainingService.create(this.rfDataModal.value).subscribe(result => {
            this.notification.success('Thành công', '', { nzPlacement: 'bottomRight' });
              this.close(true);
              localStorage.removeItem('data-training');
          });
        }
    }
  }

  close(isSave?: boolean): void {
      this.modal.destroy(isSave);
  }

}

