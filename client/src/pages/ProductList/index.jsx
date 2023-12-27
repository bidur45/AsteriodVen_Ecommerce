import { ProCard, ProForm } from '@ant-design/pro-components';
import { Button, notification, Table } from 'antd';
import { useEffect, useRef, useState } from 'react';
import { history } from 'umi';

import API from '../../services/ECommerce/index';
export default () => {
  const formRef = useRef(null);
  const [products, setProducts] = useState();
  const onAdd = () => {
    history.push('Product');
  };

  const onEdit = async (record) => {
    const Id = record?.id;
    history.push('Product', { Id });
  };

  const onDelete = async (record) => {
    try {
      const resDel = await API.product.delete(record.id);
      resDel && resDel?.isSuccess === true ? onSuccess(resDel) : onFailed(resDel);
    } catch (ex) {
      notification['error']({
        message: 'Error',
        description: ex.message,
      });
    }
  };

  const onSuccess = async (res) => {
    notification['success']({
      message: 'Success',
      description: res?.message,
    });
    await getProductsList();
  };

  const onFailed = (res) => {
    notification['error']({
      message: 'Error',
      description: res?.message,
    });
  };
  const productsColumn = [
    {
      title: 'SNo.',
      dataIndex: 'id',
      render: (_, __, z) => z + 1,
    },
    {
      title: 'Name',
      dataIndex: 'name',
    },
    {
      title: 'Description',
      dataIndex: 'description',
    },
    {
      title: 'Stock',
      dataIndex: 'stock',
    },
    {
      title: 'Price',
      dataIndex: 'price',
    },
    {
      title: 'Image',
      dataIndex: 'img',
      render: (value) => {
        return (
          <>
            <img
              src={value}
              alt="avatar"
              style={{
                width: '10%',
                height: '10%',
              }}
            />
          </>
        );
      },
    },
    {
      title: 'Action',
      render: (record) => {
        return (
          <>
            <Button type="primary" onClick={() => onEdit(record)}>
              {' '}
              Edit
            </Button>
            <Button type="primary" danger onClick={() => onDelete(record)}>
              {' '}
              Delete
            </Button>
          </>
        );
      },
    },
  ];

  const getProductsList = async () => {
    const resProduct = await API.product.get();
    setProducts(resProduct?.data?.map((x) => x));
  };

  useEffect(() => {
    const fetchData = async () => {
      await getProductsList();
    };
    fetchData();
  }, []);

  return (
    <>
      <ProCard>
        <ProForm formRef={formRef} submitter={false}>
          <Button onClick={onAdd} type="primary">
            Add
          </Button>
          <Table dataSource={products} columns={productsColumn} rowKey={'id'} />
        </ProForm>
      </ProCard>
    </>
  );
};
