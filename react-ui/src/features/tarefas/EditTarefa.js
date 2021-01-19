import React from 'react';
import { 
    Button,
    Container,
    CssBaseline,
    TextField,
    Typography
} from '@material-ui/core';
import Dashboard from '../../layout/Dashboard';
import { updateAsync } from './tarefasSlice';
import { connect } from 'react-redux';

const mapDispatchToProps = () => {
    return {
        updateAsync
    };
};

const mapStateToProps = state => ({
    tarefas: state.tarefas.tarefas
})
  
class EditTarefa extends React.Component {
    constructor(props) {
        super(props)

        const { match: { params } } = this.props;
        const { id } = params;

        const { tarefas } = this.props;
        const { descricao, dataConclusao, dataEntrega } = tarefas.find(t => t.id == id);
    
        this.state = {
            id,
            descricao,
            descricaoErrorText: '',
            dataConclusao,
            dataEntrega
        }
    }

    isValidDescricao = () => {
        return this.state.descricaoErrorText.trim().length === 0;
    }

    validateDescricao = (e) => {
        var descricaoErrorText = '';
        if (e.target.value.trim().length === 0)
            descricaoErrorText = 'Campo obrigatório'
        this.setState({ descricaoErrorText })
    }

    handleDescricaoChange = (e) => {
        this.setState({ descricao: e.target.value });
    }

    handleDataEntregaChange = (e) => {
        this.setState({ dataEntrega: e.target.value });
    }

    handleDataConclusaoChange = (e) => {
        this.setState({ dataConclusao: e.target.value });
    }

    handleUpdate = async (e) => {
        e.preventDefault();
        const { id, descricao, dataConclusao, dataEntrega } = this.state;
        const { updateAsync, history } = this.props;
        
        await updateAsync(id, { id, descricao, dataConclusao, dataEntrega });
        history.push('/');
    }
    render() {
        const { descricao, dataConclusao, dataEntrega } = this.state;

        return (
            <Dashboard>
                <Container
                    component="main"
                    maxWidth="xs"
                >
                    <CssBaseline />
                    <Typography component="h1" variant="h5">
                        Editar Tarefa
                    </Typography>
                    <form noValidate onSubmit={this.handleUpdate}>
                        <TextField
                            type="text"
                            variant="outlined"
                            margin="normal"
                            required
                            multiline
                            fullWidth
                            id="descricao"
                            label="Descrição"
                            name="descricao"
                            error={!this.isValidDescricao()}
                            autoFocus
                            defaultValue={descricao}
                            helperText={this.state.descricaoErrorText}
                            onChange={this.handleDescricaoChange}
                            onBlur={this.validateDescricao}
                        />
                        <TextField
                            type="datetime-local"
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            defaultValue={dataConclusao}
                            id="dataConclusao"
                            label="Data da Conclusão"
                            name="dataConclusao"
                        />
                        <TextField
                            type="datetime-local"
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            defaultValue={dataEntrega}
                            id="dataEntrega"
                            label="Data de Entrega"
                            name="dataEntrega"
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            disabled={!this.isValidDescricao()}
                        >
                            Salvar
                        </Button>
                    </form>
                </Container>
            </Dashboard>
        )
    }
}

export default connect(mapStateToProps, mapDispatchToProps())(EditTarefa)