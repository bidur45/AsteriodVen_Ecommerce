/**
 * @name Proxy Configuration
 * @see The proxy cannot take effect in the production environment.
 * Therefore, there is no configuration for the production environment.
 * For details, please refer to:
 * https://pro.ant.design/docs/deploy
 *
 * @doc https://umijs.org/docs/guides/proxy
 */
export default {
  // If you need to customize the local development server, uncomment and adjust as needed
  // dev: {
  //   // localhost:8000/api/** -> https://preview.pro.ant.design/api/**
  //   '/api/': {
  //     // The target to proxy
  //     target: 'https://preview.pro.ant.design',
  //     // This configuration allows for proxying from http to https
  //     // This might be needed for features that depend on origin, like cookies
  //     changeOrigin: true,
  //   },
  // },

  /**
   * @name Detailed Proxy Configuration
   * @doc https://github.com/chimurai/http-proxy-middleware
   */
  test: {
    // localhost:8000/api/** -> https://preview.pro.ant.design/api/**
    '/api/': {
      target: 'https://proapi.azurewebsites.net',
      changeOrigin: true,
      pathRewrite: { '^': '' },
    },
  },
  pre: {
    '/api/': {
      target: 'your pre url',
      changeOrigin: true,
      pathRewrite: { '^': '' },
    },
  },
};
