import api from '../genericService';
export default {
  auth: api('/Auth'),
  currentUser: api('/Auth/CurrentUser'),
  product: api('/Product'),
};
