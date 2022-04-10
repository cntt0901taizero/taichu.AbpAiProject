import type { AiTrainingDto, AiTrainingPagedAndSortedResultRequestDto } from './dto/models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AiTrainingService {
  apiName = 'Default';

  create = (input: AiTrainingDto) =>
    this.restService.request<any, AiTrainingDto>({
      method: 'POST',
      url: '/api/app/ai-training',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/ai-training/${id}`,
    },
    { apiName: this.apiName });

  get = (id: number) =>
    this.restService.request<any, AiTrainingDto>({
      method: 'GET',
      url: `/api/app/ai-training/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: AiTrainingPagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<AiTrainingDto>>({
      method: 'GET',
      url: '/api/app/ai-training',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: number, input: AiTrainingDto) =>
    this.restService.request<any, AiTrainingDto>({
      method: 'PUT',
      url: `/api/app/ai-training/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
