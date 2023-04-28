import '@/styles/globals.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import type { AppProps } from 'next/app'
import {wrapper} from "@/store/store";
import React, {useEffect} from "react";
import {loadMetamodel} from "@/features/metamodel/metamodel-slice";
import {Provider} from "react-redux";
import {PersistGate} from "redux-persist/integration/react";
import {persistStore} from "redux-persist";
import {Layout} from "@/components/layout/Layout";

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
              <Layout>
                  <Component {...pageProps} />
              </Layout>
          </PersistGate>
      </Provider>
  )
}

export default App;