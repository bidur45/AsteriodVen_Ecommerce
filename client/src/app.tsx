import Footer from '@/components/Footer';
import RightContent from '@/components/RightContent';
import { LinkOutlined } from '@ant-design/icons';
import type { Settings as LayoutSettings } from '@ant-design/pro-components';
import { SettingDrawer } from '@ant-design/pro-components';
import type { RunTimeLayoutConfig } from '@umijs/max';
import { history, Link } from '@umijs/max';
import defaultSettings from '../config/defaultSettings';
import { errorConfig } from './requestErrorConfig';
import API from './services/ECommerce';
const isDev = process.env.NODE_ENV === 'development';
const loginPath = '/user/login';

/**
 * @see  https://umijs.org/zh-CN/plugins/plugin-initial-state
 * */
export async function getInitialState(): Promise<{
  settings?: Partial<LayoutSettings>;
  currentUser?: API.CurrentUser;
  loading?: boolean;
  fetchUserInfo?: () => Promise<API.CurrentUser | undefined>;
}> {
  // const fetchMenuData = async (userId: any) => {
  //   console.log('uid', userId);
  //   try {
  //     const res = await API.menu(userId);
  //     return res;
  //   } catch (error) {
  //     history.push(loginPath);
  //   }
  //   return undefined;
  // };

  const fetchUserInfo = async () => {
    try {
      const res = await API.currentUser.get();
      console.log(res);
      return { ...res?.data };
    } catch (error) {
      history.push(loginPath);
    }
    return undefined;
  };
  // If it is not a login page, execute
  const { location } = history;
  if (location.pathname !== loginPath) {
    const currentUser = await fetchUserInfo();
    return {
      fetchUserInfo,
      currentUser,
      settings: defaultSettings as Partial<LayoutSettings>,
    };
  }
  return {
    fetchUserInfo,
    settings: defaultSettings as Partial<LayoutSettings>,
  };
}

// ProLayout APIs supported API https://procomponents.ant.design/components/layout
export const layout: RunTimeLayoutConfig = ({ initialState, setInitialState }) => {
  return {
    rightContentRender: () => <RightContent />,
    waterMarkProps: {
      content: initialState?.currentUser?.name,
    },
    footerRender: () => <Footer />,
    onPageChange: () => {
      const { location } = history;
      // If not logged in, redirect to login
      if (!initialState?.currentUser && location.pathname !== loginPath) {
        history.push(loginPath);
      }
    },
    layoutBgImgList: [
      {
        src: 'https://mdn.alipayobjects.com/yuyan_qk0oxh/afts/img/D2LWSqNny4sAAAAAAAAAAAAAFl94AQBr',
        left: 85,
        bottom: 100,
        height: '303px',
      },
      {
        src: 'https://mdn.alipayobjects.com/yuyan_qk0oxh/afts/img/C2TWRpJpiC0AAAAAAAAAAAAAFl94AQBr',
        bottom: -68,
        right: -45,
        height: '303px',
      },
      {
        src: 'https://mdn.alipayobjects.com/yuyan_qk0oxh/afts/img/F6vSTbj8KpYAAAAAAAAAAAAAFl94AQBr',
        bottom: 0,
        left: 0,
        width: '331px',
      },
    ],
    links: isDev
      ? [
          <Link key="openapi" to="/umi/plugin/openapi" target="_blank">
            <LinkOutlined />
            {/* <span>OpenAPI document</span> */}
          </Link>,
        ]
      : [],
    menuHeaderRender: undefined,
    // Custom 403 page
    // unAccessible: <div>unAccessible</div>,
    //Add a loading state
    childrenRender: (children) => {
      // if (initialState?.loading) return <PageLoading />;
      return (
        <>
          {children}
          <SettingDrawer
            disableUrlParams
            enableDarkTheme
            settings={initialState?.settings}
            onSettingChange={(settings) => {
              setInitialState((preInitialState) => ({
                ...preInitialState,
                settings,
              }));
            }}
          />
        </>
      );
    },
    // menu: {
    //   // Re-execute request whenever initialState?.currentUser?.userid is modified
    //   params: {
    //     userId: initialState?.currentUser?.userId,
    //   },
    //   request: async () => {
    //     // initialState.currentUser contains all user information
    //     // console.log('menu',initialState?.currentUser);
    //     // const menuData = await fetchMenuData(
    //     //     initialState?.currentUser?.userId
    //     // );
    //     //  return null;
    //     //return initialState?.currentUser?.menus ?? null;
    //     //const menu = localStorage.getItem('menus');
    //    // return menu !== null ? JSON.parse(menu) : null;
    //    return null;
    //   },
    //   autoClose: true,
    // },
    ...initialState?.settings,
  };
};

/**
 * @name request configuration, you can configure error handling
 * It provides a unified network request and error handling scheme based on the useRequest of axios and ahooks.
 * @doc https://umijs.org/docs/max/request#configuration
 */
export const request = {
  ...errorConfig,
};
