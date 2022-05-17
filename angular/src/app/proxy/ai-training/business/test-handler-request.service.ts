import type { TestRequest } from './models';
import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TestHandlerRequestService {
  apiName = 'Default';

  handleByRequestAndCancellationToken = (request: TestRequest, cancellationToken: any) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/test-handler-request/handle',
      body: request,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
