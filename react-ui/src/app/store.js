import { configureStore } from '@reduxjs/toolkit';
import counterReducer from '../features/counter/counterSlice';
import tarefaReducer from '../features/tarefas/tarefasSlice';

export default configureStore({
  reducer: {
    counter: counterReducer,
    tarefas: tarefaReducer
  },
});
