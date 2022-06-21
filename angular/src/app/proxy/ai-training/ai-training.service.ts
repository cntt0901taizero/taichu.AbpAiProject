import type { AiTrainingDto, AiTrainingPagedAndSortedResultRequestDto } from './dto/models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { FileDto } from '../base-application/dtos/models';

@Injectable({
  providedIn: 'root',
})
export class AiTrainingService {
  apiName = 'Default';

  create = (input: AiTrainingDto) =>
    this.restService.request<any, AiTrainingDto>({
      method: 'POST',
      url: '/api/app/AiTraining/Create',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: number) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/AiTraining/RemoveById/${id}`,
    },
    { apiName: this.apiName });

  get = (id: number) =>
    this.restService.request<any, AiTrainingDto>({
      method: 'GET',
      url: `/api/app/AiTraining/GetById/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: AiTrainingPagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<AiTrainingDto>>({
      method: 'POST',
      url: '/api/app/AiTraining/GetList',
      body: input,
    },
    { apiName: this.apiName });

  getListToFile = (isAll: boolean, fileType: number, input: AiTrainingPagedAndSortedResultRequestDto) =>
    this.restService.request<any, FileDto>({
      method: 'POST',
      url: '/api/app/AiTraining/GetListToFile',
      params: { isAll, fileType },
      body: input,
    },
    { apiName: this.apiName });

  test = () =>
    this.restService.request<any, number>({
      method: 'POST',
      url: '/api/app/ai-training/test',
    },
    { apiName: this.apiName });

  update = (id: number, input: AiTrainingDto) =>
    this.restService.request<any, AiTrainingDto>({
      method: 'POST',
      url: `/api/app/AiTraining/Update/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
