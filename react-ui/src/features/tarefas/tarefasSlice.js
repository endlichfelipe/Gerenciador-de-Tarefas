import { createSlice } from '@reduxjs/toolkit';
import api from '../../services/api'

export const tarefaSlice = createSlice({
    name: 'tarefas',
    initialState: {
        tarefas: []
    },
    reducers: {
        remove: (state, action) => {
            state.tarefas = state.tarefas.filter(t => t.id !== action.payload);
        },

        add: (state, action) => {
            state.tarefas.push(action.payload);
        },
        
        append: (state, action) => {  
            state.tarefas = action.payload;
        },

        update: (state, action) => {
            const tarefa = action.payload;
            state.tarefas = state.tarefas.filter(t => t.id !== tarefa.id).push(tarefa);
        }
    }
})

export const { remove, add, append, update, } = tarefaSlice.actions;

export const fetchAsync = () => dispatch => {
    api.get('tarefa')
        .then(res => {
            const tarefas = res.data;
            dispatch(append(tarefas));
        })
}

export const deleteAsync = (id) => dispatch => {
    api.delete(`tarefa/${id}`)
        .then(res => {
            if (res.status >= 200 && res.status < 300)
                dispatch(remove(id));
            });
}

export const addAsync = (tarefa) => dispatch => {
    api.post(`tarefa`, tarefa)
        .then(res => {
            if (res.status >= 200 && res.status < 300)
                dispatch(add(res.data));
            });
}

export const updateAsync = (id, tarefa) => dispatch => {
    tarefa.id = parseInt(tarefa.id);
    api.put(`tarefa/${id}`, tarefa)
        .then(res => {
            if (res.status >= 200 && res.status < 300)
            dispatch(update(tarefa));
        })
}

export const getAll = state => state.tarefas.tarefas;
export const getById = state => id => state.tarefas.tarefas.find(t => t.id === id);

export default tarefaSlice.reducer;