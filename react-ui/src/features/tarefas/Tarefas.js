import React from 'react';
import api from '../../services/api';
import EditIcon from "@material-ui/icons/Edit";
import Dashboard from '../../layout/Dashboard';
import DeleteIcon from "@material-ui/icons/Delete";
import Dialog from '@material-ui/core/Dialog';
import IconButton from '@material-ui/core/IconButton';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import { Link } from 'react-router-dom';
import { Button, DialogActions, DialogContent, DialogContentText, DialogTitle } from '@material-ui/core';
import { fetchAsync, deleteAsync } from './tarefasSlice';
import { connect } from 'react-redux';

const mapDispatchToProps = () => {
    return {
        fetchAsync,
        deleteAsync
    };
};

const mapStateToProps = state => ({
    tarefas: state.tarefas.tarefas
})

class DeleteTarefaModal extends React.Component {
    state = {
        open: false,
    }

    handleOpen = () => {
        const open = true;
        this.setState({ open });
    }

    handleClose = () => {
        const open = false;
        this.setState({ open });
    }

    handleDelete = async () => {
        const { tarefa, removeFunc } = this.props;
        await removeFunc(tarefa.id);
        this.handleClose();
    }

    render() {
        const { open } = this.state;

        return(
            <React.Fragment>
                <IconButton onClick={this.handleOpen}>
                    <DeleteIcon />
                </IconButton>
                <Dialog
                    open={open}
                    onClose={this.handleClose}
                >
                    <DialogTitle>Remover tarefa</DialogTitle>
                    <DialogContent>
                        <DialogContentText>
                            Tem certeza que desejar remover essa tarefa?
                        </DialogContentText>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={this.handleClose}>
                            Não
                        </Button>
                        <Button onClick={this.handleDelete}>
                            Sim
                        </Button>
                    </DialogActions>
                </Dialog>
            </React.Fragment>
        )
    }
}

class EnhancedTableHead extends React.Component {
    render() {
        return (
            <TableHead>
                <TableRow>
                    <TableCell>ID</TableCell>
                    <TableCell>Descrição</TableCell>
                    <TableCell>Data de Entrega</TableCell>
                    <TableCell>Data de Conclusão</TableCell>
                    <TableCell>Actions</TableCell>
                </TableRow>
            </TableHead>
        );
    }
}

class EnhancedTableBody extends React.Component {
    render() {
        const { tarefas, removeFunc } = this.props;

        return(
            <TableBody>
                {tarefas.map((tarefa, i) =>
                    <TableRow key={i}>
                        <TableCell>{tarefa.id}</TableCell>
                        <TableCell>{tarefa.descricao}</TableCell>
                        <TableCell>{tarefa.dataEntrega}</TableCell>
                        <TableCell>{tarefa.dataConclusao}</TableCell>
                        <TableCell>
                            <IconButton component={ Link } to={`/tarefa/edit/${tarefa.id}`} ><EditIcon /></IconButton>
                            <DeleteTarefaModal tarefa={tarefa} removeFunc={removeFunc} />
                        </TableCell>
                    </TableRow>
                )}
            </TableBody>
        )
    }
}

class Tarefas extends React.Component {
    async componentDidMount() {
        const { fetchAsync } = this.props;
        await fetchAsync();
    }

    render() {
        const { tarefas, deleteAsync } = this.props;
        return (
            <Dashboard>
                <Table>
                    <EnhancedTableHead />
                    <EnhancedTableBody removeFunc={deleteAsync} tarefas={tarefas} />
                </Table>
            </Dashboard>
        )
    }
}

export default connect(mapStateToProps, mapDispatchToProps())(Tarefas);