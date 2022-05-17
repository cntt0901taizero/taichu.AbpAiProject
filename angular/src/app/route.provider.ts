import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/ai-training',
        name: '::Menu:AiTraining',
        iconClass: 'fas fa-book',
        order: 2,
        layout: eLayoutType.application,
        requiredPolicy: 'AbpAiProject.AiTraining'
      },
      {
        path: '/data-training',
        name: '::Menu:AiTraining:DataTraining',
        iconClass: 'fas fa-book',
        parentName: '::Menu:AiTraining',
        layout: eLayoutType.application,
        requiredPolicy: 'AbpAiProject.DataTraining'
      },
    ]);
  };
}
