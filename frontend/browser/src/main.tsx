import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import {StoreContextProvider} from "./store/StoreContext.tsx";

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <StoreContextProvider>
          <App />
      </StoreContextProvider>
  </React.StrictMode>,
);
