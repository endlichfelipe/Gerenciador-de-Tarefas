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

        add: state => tarefa => {
            console.log('Adding')
        },
        
        append: (state, action) => {  
            state.tarefas = action.payload;
        },

        update: state => (id, tarefa) => {
            console.log('Updating')
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
            console.log(res.status)
            if (res.status >= 200 && res.status < 300)
                dispatch(remove(id));
            });
}

export const getAll = state => state.tarefas.tarefas;
export const getById = state => id => state.tarefas.tarefas.find(t => t.id === id);

export default tarefaSlice.reducer;