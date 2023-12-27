import { ProLayoutProps } from '@ant-design/pro-components';

/**
 * @name
 */
const Settings: ProLayoutProps & {
  pwa?: boolean;
  logo?: string;
} = {
  theme: {
    // Specify the theme colors here
    light: {
      '@primary-color': 'red !important', // Use the colorPrimary from your settings
      // You can set other theme colors as well
      // For example:
      '@layout-header-background': '#1890ff !important', // Background color for the header
      '@layout-sider-background': '#f0f2f5 !important', // Background color for the siderbar
    },
  },
  // 拂晓蓝
  colorPrimary: '#1890ff',
  layout: 'mix',
  contentWidth: 'Fluid',
  fixedHeader: false,
  fixSiderbar: true,
  colorWeak: false,
  title: 'E-Commerce',
  pwa: true,
  //logo: 'https://png.pngtree.com/png-vector/20210122/ourmid/pngtree-indian-worship-kalasha-vector-png-image_2786459.jpg',
  iconfontUrl: '',
  token: {
    // See TS declaration, demo see documentation, modify the style by token
    //https://procomponents.ant.design/components/layout#%E9%80%9A%E8%BF%87-token-%E4%BF%AE%E6%94%B9%E6%A0%B7%E5%BC%8F
  },
};

export default Settings;
