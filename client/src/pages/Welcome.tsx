import { useModel } from '@umijs/max';
import React from 'react';
// import Dashboard from '../components/Dashboard';

/**
 * For each individual card, components are extracted for reusable styles
 * @param param0
 * @returns
 */

const Welcome: React.FC = () => {
  // const { token } = theme.useToken();
  const { initialState } = useModel('@@initialState');
  console.log('ini', initialState);

  return (
    // <Dashboard />
 <>WellCome</>
    // <PageContainer>
    //   <Card
    //     style={{
    //       borderRadius: 8,
    //     }}
    //   ></Card>
    // </PageContainer>
  );
};

export default Welcome;
