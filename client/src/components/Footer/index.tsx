import { GithubOutlined } from '@ant-design/icons';
import { DefaultFooter } from '@ant-design/pro-components';
import { useIntl } from '@umijs/max';
import React from 'react';

const Footer: React.FC = () => {
  const intl = useIntl();
  const defaultMessage = intl.formatMessage({
    id: 'app.copyright.produced',
    defaultMessage: 'Produced by Kalash Technologies Pvt. Ltd.',
  });

  const currentYear = new Date().getFullYear();

  return (
    <DefaultFooter
      style={{
        background: 'none', // Remove the default background
        backgroundColor: '#3e8ec0', // Set your desired background color
        textAlign: 'center', // Center the content horizontally
        display: 'none', // Use flexbox for alignment
        flexDirection: 'column', // Align content vertically
        alignItems: 'center', // Center content horizontally
        justifyContent: 'center', // Center content vertically
        minHeight: '50px', // Set minimum height to adjust content
        maxHeight:'60px',
        fontSize:'15px',
        paddingTop:'-100px',
      }}
      copyright={`${currentYear} ${defaultMessage}`} // Set the copyright info
      links={[
        {
          key: 'kalash',
          title: 'Kalash Coop', // Custom link title
          href: 'https://pro.ant.design', // Custom link URL
          blankTarget: true,
        },
        /*{
          key: 'github',
          title: <GithubOutlined />, // Custom link title with an icon
          href: 'https://github.com/ant-design/ant-design-pro',
          blankTarget: true,
        },*/
      ]}
    />
  );
};

export default Footer;
