import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface AiTrainingDto extends EntityDto<number> {
  inputString?: string;
  outputString?: string;
  funcName?: string;
  link?: string;
  note?: string;
}

export interface AiTrainingPagedAndSortedResultRequestDto extends PagedAndSortedResultRequestDto {
  pageNumber: number;
}
