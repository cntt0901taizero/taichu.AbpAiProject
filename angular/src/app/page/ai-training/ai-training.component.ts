import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AiTrainingService } from '@proxy/ai-training';
import { AiTrainingDto, AiTrainingPagedAndSortedResultRequestDto } from '@proxy/ai-training/dto';
import { NzModalService } from 'ng-zorro-antd/modal';
import { UpsertDatatrainingComponent } from './components/upsert-datatraining/upsert-datatraining.component';

@Component({
  selector: 'app-ai-training',
  templateUrl: './ai-training.component.html',
  styleUrls: ['./ai-training.component.scss'],
  providers: [ListService]
})
export class AiTrainingComponent implements OnInit {
  formGroup: FormGroup;
  filter: AiTrainingPagedAndSortedResultRequestDto;
  data: PagedResultDto<AiTrainingDto> = new PagedResultDto<AiTrainingDto>();
  loadingIndicator = false;

  constructor(
    public readonly list: ListService,
    private _aiTrainingService: AiTrainingService,
    private modalService: NzModalService,
    private fb: FormBuilder,
  ) { 
    this.filter = {
      maxResultCount: 10,
      skipCount: 0,
      pageNumber: 0,
    };
    this.formGroup = this.fb.group({
      filter: [''],
    });
  }

  ngOnInit(): void {
    this.search();
  }

  openUpsertModal(dataItem?: any) {
    const modal = this.modalService.create({
      nzTitle: dataItem ? 'Sửa thông tin' : 'Thêm mới',
      nzContent: UpsertDatatrainingComponent,
      nzComponentParams: {
        dataItem: dataItem ? dataItem : {}
      },
      nzFooter: null
    });
    modal.afterClose.subscribe(result => {
        if (result) {
            this.search();
        }
    });
  }

  delete(dataItem: any): void {
    this.modalService.confirm({
      nzTitle: 'Xóa dữ liệu',
      nzContent: 'Bạn có chắc chắn muốn xóa?',
      nzOkText: 'Chọn',
      nzCancelText: 'Hủy',
      nzOnOk: () => {
        this._aiTrainingService.delete(dataItem.id).subscribe(() => {
        })
      },
      nzOnCancel: () => {

      }
    });
  }

  search() {
    this.filter = {
      ...this.filter,
      ...this.formGroup.value
    };
    this.filter.pageNumber = 0;
    this.getGrid(null);
    
  }

  clearFilter() {
    this.formGroup.reset();
    this.search();
  }

  getGrid(pageInfo) {
    this.loadingIndicator = true;
    if (pageInfo) {
      this.filter.skipCount = pageInfo?.offset * pageInfo?.limit;
      this.filter.maxResultCount = pageInfo?.limit;
      this.filter.pageNumber = pageInfo?.offset;
    }
    const dataGrid = () => this._aiTrainingService.getList(this.filter);
    this.list.hookToQuery(dataGrid).subscribe((res) => {
      this.data = res;
    }, (err) => {
      this.loadingIndicator = false;
    }, () => {
      this.loadingIndicator = false;
    });
  }

}
