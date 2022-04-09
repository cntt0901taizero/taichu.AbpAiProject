const ENV = {
  dev: {
    apiUrl: 'http://localhost:44384',
    oAuthConfig: {
      issuer: 'http://localhost:44372',
      clientId: 'AbpAiProject_App',
      clientSecret: '1q2w3e*',
      scope: 'offline_access AbpAiProject',
    },
    localization: {
      defaultResourceName: 'AbpAiProject',
    },
  },
  prod: {
    apiUrl: 'http://localhost:44384',
    oAuthConfig: {
      issuer: 'http://localhost:44372',
      clientId: 'AbpAiProject_App',
      clientSecret: '1q2w3e*',
      scope: 'offline_access AbpAiProject',
    },
    localization: {
      defaultResourceName: 'AbpAiProject',
    },
  },
};

export const getEnvVars = () => {
  // eslint-disable-next-line no-undef
  return __DEV__ ? ENV.dev : ENV.prod;
};
