import { defineConfig } from '@umijs/max';
import { join } from 'path';
import defaultSettings from './defaultSettings';
import proxy from './proxy';
import routes from './routes';

const { REACT_APP_ENV = 'dev' } = process.env;

export default defineConfig({
  /**
   * @name Enable Hash Mode
   * @description Include hash suffix in the build artifacts. Usually used for incremental releases and avoiding browser caching.
   * @doc https://umijs.org/docs/api/config#hash
   */
  hash: true,

  /**
   * @name Compatibility Settings
   * @description Setting for IE11 compatibility, not necessarily perfect. Check all dependencies used.
   * @doc https://umijs.org/docs/api/config#targets
   */
  // targets: {
  //   ie: 11,
  // },
  /**
   * @name Route Configuration
   * @description Configure routes. Files not imported in routes won't be compiled.
   * @doc https://umijs.org/docs/guides/routes
   */
  // umi routes: https://umijs.org/docs/routing
  routes,
  /**
   * @name Theme Configuration
   * @description Although called a theme, it's actually just setting variables for LESS.
   * @doc Customize Ant Design theme https://ant.design/docs/react/customize-theme
   * @doc Umi theme configuration https://umijs.org/docs/api/config#theme
   */
  theme: {
    // If you don't want to dynamically set the theme with configProvide, set this to 'default'.
    // Only when set to 'variable', you can use configProvide to dynamically set the primary color.
    'root-entry-name': 'variable',
  },
  /**
   * @name moment Internationalization Configuration
   * @description If internationalization is not required, enabling this can reduce the size of JavaScript bundles.
   * @doc https://umijs.org/docs/api/config#ignoremomentlocale
   */
  ignoreMomentLocale: true,
  /**
   * @name Proxy Configuration
   * @description Allows your local server to proxy requests to your server, enabling access to server data.
   * @see Note that proxies can only be used during local development and won't work after build.
   * @doc Proxy introduction https://umijs.org/docs/guides/proxy
   * @doc Proxy configuration https://umijs.org/docs/api/config#proxy
   */
  proxy: proxy[REACT_APP_ENV as keyof typeof proxy],
  /**
   * @name Fast Refresh Configuration
   * @description A good hot update component that preserves state during updates.
   */
  fastRefresh: true,
  //============== Below are max plugin configurations ===============
  /**
   * @name Data Flow Plugin
   * @@doc https://umijs.org/docs/max/data-flow
   */
  model: {},
  /**
   * A global initial data flow for sharing data between plugins.
   * @description Used to store global data such as user information or global states.
   * Global initial state is created at the very beginning of the entire Umi project.
   * @doc https://umijs.org/docs/max/data-flow#%E5%85%A8%E5%B1%80%E5%88%9D%E5%A7%8B%E7%8A%B6%E6%80%81
   */
  initialState: {},
  /**
   * @name Layout Plugin
   * @doc https://umijs.org/docs/max/layout-menu
   */
  title: 'Ant Design Pro',
  layout: {
    locale: true,
    ...defaultSettings,
  },
  /**
   * @name moment2dayjs Plugin
   * @description Replaces moment with dayjs in the project.
   * @doc https://umijs.org/docs/max/moment2dayjs
   */
  moment2dayjs: {
    preset: 'antd',
    plugins: ['duration'],
  },
  /**
   * @name Internationalization Plugin
   * @doc https://umijs.org/docs/max/i18n
   */
  locale: {
    // default zh-CN
    default: 'zh-CN',
    antd: true,
    // default true, when it is true, will use `navigator.language` to overwrite default
    baseNavigator: true,
  },
  /**
   * @name antd Plugin
   * @description Includes the babel import plugin internally.
   * @doc https://umijs.org/docs/max/antd#antd
   */
  antd: {},
  /**
   * @name Network Request Configuration
   * @description Provides a unified network request and error handling solution based on axios and ahooks' useRequest.
   * @doc https://umijs.org/docs/max/request
   */
  request: {},
  /**
   * @name Access Plugin
   * @description Permissions plugin based on initialState. Must enable initialState first.
   * @doc https://umijs.org/docs/max/access
   */
  access: {},
  /**
   * @name Additional <head> Scripts
   * @description Configure additional scripts in <head>.
   */
  headScripts: [
    // Solve the issue of white screen on initial load
    { src: '/scripts/loading.js', async: true },
  ],
  //================ pro Plugin Configuration =================
  presets: ['umi-presets-pro'],
  /**
   * @name openAPI Plugin Configuration
   * @description Generates serve and mock based on the openapi specification, reducing boilerplate code.
   * @doc https://pro.ant.design/zh-cn/docs/openapi/
   */
  openAPI: [
    {
      requestLibPath: "import { request } from '@umijs/max'",
      // Alternatively, use the online version
      // schemaPath: "https://gw.alipayobjects.com/os/antfincdn/M%24jrzTTYJN/oneapi.json"
      schemaPath: join(__dirname, 'oneapi.json'),
      mock: false,
    },
    {
      requestLibPath: "import { request } from '@umijs/max'",
      schemaPath: 'https://gw.alipayobjects.com/os/antfincdn/CA1dOm%2631B/openapi.json',
      projectName: 'swagger',
    },
  ],
  mfsu: {
    strategy: 'normal',
  },
  requestRecord: {},
});
