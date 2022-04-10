import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { AiTrainingService } from '@proxy/ai-training';
import { AiTrainingDto } from '@proxy/ai-training/dto';

@Component({
  selector: 'app-ai-training',
  templateUrl: './ai-training.component.html',
  styleUrls: ['./ai-training.component.scss'],
  providers: [ListService]
})
export class AiTrainingComponent implements OnInit {
  data: PagedResultDto<AiTrainingDto> = new PagedResultDto<AiTrainingDto>()

  constructor(
    public readonly list: ListService,
    private _aiTrainingService: AiTrainingService
  ) { }

  ngOnInit(): void {
    const dataGrid = (param) => this._aiTrainingService.getList(param);
    this.list.hookToQuery(dataGrid).subscribe((res) => {
      this.data = res;
    });
  }

}
