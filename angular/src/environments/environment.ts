import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'AbpAiProject',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44372',
    redirectUri: baseUrl,
    clientId: 'AbpAiProject_App',
    responseType: 'code',
    scope: 'offline_access AbpAiProject',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44384',
      rootNamespace: 'taichu.AbpAiProject',
    },
  },
} as Environment;
