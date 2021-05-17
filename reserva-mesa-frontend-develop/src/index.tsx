import { runWithAdal } from 'react-adal';
import reportWebVitals from './reportWebVitals';

import { auth } from './services';


runWithAdal(
  auth.authContext,
  () => {
    require('./indexApp');
  },
  false
)

reportWebVitals();
