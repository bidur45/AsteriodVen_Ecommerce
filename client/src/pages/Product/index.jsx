import { LoadingOutlined, PlusOutlined } from '@ant-design/icons';
import { ProCard, ProForm, ProFormDigit, ProFormText } from '@ant-design/pro-components';
import { Button, Form, notification, Typography, Upload } from 'antd';
import { useEffect, useRef, useState } from 'react';
import { history, useLocation } from 'umi';
import API from '../../services/ECommerce/index';
const { Title } = Typography;
export default () => {
  let location = useLocation();
  const [loading, setLoading] = useState(false);
  const [action, setAction] = useState('A');
  const formRef = useRef(null);
  const [imgLoading, setImgLoading] = useState(false);
  const [imageUrl, setImageUrl] = useState();

  const getBase64 = (img, callback) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
  };

  const beforeImgUpload = (file) => {
    const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
    if (!isJpgOrPng) {
      message.error('You can only upload JPG/PNG file!');
    }
    const isLt2M = file.size / 1024 / 1024 < 2;
    if (!isLt2M) {
      message.error('Image must smaller than 2MB!');
    }
    return isJpgOrPng && isLt2M;
  };

  const handleImgChange = (info) => {
    if (info.file.status === 'uploading') {
      setImgLoading(true);
      info.file.status = 'done';
    }
    if (info.file.status === 'done') {
      getBase64(info.file.originFileObj, (url) => {
        setImgLoading(false);
        setImageUrl(url);
        formRef?.current?.setFieldsValue({ img: url });
      });
    }
  };

  const uploadImgButton = (
    <div>
      {imgLoading ? <LoadingOutlined /> : <PlusOutlined />}
      <div
        style={{
          marginTop: 8,
        }}
      >
        Upload
      </div>
    </div>
  );

  const getProductDetails = async (id) => {
    const resproductDetails = await API.product.getById(id);
    const productDtl = resproductDetails?.data;
    formRef?.current?.setFieldsValue({
      ...productDtl,
    });
    setImageUrl(productDtl?.img);
    setAction('E');
  };
  useEffect(() => {
    const FetchData = async () => {
      const Id = location?.state?.Id;

      if (Id !== undefined) {
        await getProductDetails(Id);
      }
    };

    FetchData();
  }, []);

  const reset = async () => {
    await formRef.current?.resetFields();
    setAction('A');
  };
  const onSuccess = async (res) => {
    setLoading(false);
    await reset();
    notification['success']({
      message: 'Success',
      description: res?.message,
    });
    history.push('ProductList');
  };

  const onFailed = (res) => {
    notification['error']({
      message: 'Error',
      description: res?.message,
    });
    setLoading(false);
  };
  const onSubmit = async (values) => {
    const data = {
      ...values,
    };
    setLoading(true);
    try {
      if (action === 'A') {
        const resProductSave = await API.product.post(data);
        resProductSave && resProductSave?.isSuccess === true
          ? onSuccess(resProductSave)
          : onFailed(resProductSave);
      } else {
        const resProductUpdate = await API.product.put(values.id, data);
        resProductUpdate && resProductUpdate?.isSuccess === true
          ? onSuccess(resProductUpdate)
          : onFailed(resProductUpdate);
      }
    } catch (ex) {
      setLoading(false);
      notification['error']({
        message: 'Error',
        description: ex.message,
      });
    }
  };

  return (
    <>
      <ProCard>
        <ProForm
          formRef={formRef}
          onFinish={onSubmit}
          submitter={{
            render: (props, dom) => [
              <Button
                type="primary"
                key="submit"
                id="buttonSubmit"
                loading={loading}
                onClick={() => props.form?.submit?.()}
              >
                {action === 'A' ? 'Save' : 'Update'}
              </Button>,
            ],
          }}
        >
          <Typography.Title level={3}>Product</Typography.Title>

          <ProForm.Group>
            <ProFormText name="id" hidden />
            <ProFormText
              name="name"
              label="Name"
              width="md"
              placeholder=""
              rules={[{ required: true, message: 'Name is required' }]}
            />
            <ProFormText
              name="description"
              label="Description"
              width="md"
              placeholder=""
              rules={[{ required: true, message: ' Province is required' }]}
            />
            <ProFormDigit
              name="price"
              label="Price"
              placeholder={''}
              width="md"
              rules={[{ required: true, message: ' Price is required' }]}
            />
            <ProFormDigit
              name="stock"
              label="Stock"
              placeholder={''}
              width="md"
              rules={[{ required: true, message: ' Stock is required' }]}
            />

            <Form.Item name="img">
              <Upload
                name="avatar"
                listType="picture-card"
                className="avatar-uploader"
                showUploadList={false}
                beforeUpload={beforeImgUpload}
                onChange={handleImgChange}
                // action={`${baseURL}/DummyEndPoint`}
              >
                {imageUrl ? (
                  <img
                    src={imageUrl}
                    alt="avatar"
                    style={{
                      width: '100%',
                    }}
                  />
                ) : (
                  uploadImgButton
                )}
              </Upload>
            </Form.Item>
          </ProForm.Group>
        </ProForm>
      </ProCard>
    </>
  );
};
