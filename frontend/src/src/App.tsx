import React from 'react';
import './App.css';
import {FreeQuery} from "./components/FreeQuery";
import { Provider } from 'react-redux';
import { store } from './app/store';

function App() {
  return (
      <React.StrictMode>
        <Provider store={store}>
          <FreeQuery/>
        </Provider>
      </React.StrictMode>
  )
}

export default App;
