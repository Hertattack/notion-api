import '@/styles/globals.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import type { AppProps } from 'next/app'
import {wrapper} from "@/store/store";
import {useEffect} from "react";
import {loadMetamodel} from "@/features/metamodel/metamodel-slice";
import {Provider} from "react-redux";

function App({ Component, ...rest }: AppProps) {
  const { store, props } = wrapper.useWrappedStore(rest);
  const { pageProps } = props;

  useEffect(()=>{
    store.dispatch(loadMetamodel());
  },[]);

  return (
      <Provider store={store}>
        <Component {...pageProps} />
      </Provider>
  )
}

export default App;