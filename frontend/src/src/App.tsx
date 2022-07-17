import React, {useEffect} from 'react';
import './App.css';
import {FreeQuery} from "./components/FreeQuery";
import { Provider } from 'react-redux';
import { store } from './app/store';
import {loadMetamodel} from "./features/metamodel/metamodel-slice";

const App : React.FC = () => {

    useEffect(()=>{
        store.dispatch(loadMetamodel());
    },[])

    return (
      <React.StrictMode>
        <Provider store={store}>
          <FreeQuery/>
        </Provider>
      </React.StrictMode>
    )
}

export default App;