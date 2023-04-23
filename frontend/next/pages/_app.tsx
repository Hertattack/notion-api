import '@/styles/globals.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import type { AppProps } from 'next/app'
import {wrapper} from "@/store/store";
import {useEffect} from "react";
import {loadMetamodel} from "@/features/metamodel/metamodel-slice";
import {Provider} from "react-redux";
import {PersistGate} from "redux-persist/integration/react";
import {persistStore} from "redux-persist";

function App({ Component, ...rest }: AppProps) {
  const { store, props } = wrapper.useWrappedStore(rest);
  const { pageProps } = props;
  const persistor = persistStore(store);

  useEffect(()=>{
    store.dispatch(loadMetamodel());
  },[]);

  return (
      <Provider store={store}>
          <PersistGate loading={null} persistor={persistor}>
              <Component {...pageProps} />
          </PersistGate>
      </Provider>
  )
}

export default App;