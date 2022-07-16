import { createStore, applyMiddleware } from 'redux';
import reducers from './reducers';

configureStore()

export const store = createStore(reducers, {}, applyMiddleware(thunk));
