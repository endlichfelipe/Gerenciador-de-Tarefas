import React from 'react';
import api from '../../services/api';
import { LockOutlined } from '@material-ui/icons/'
import { 
    Avatar,
    Button,
    Container, 
    CssBaseline, 
    TextField,
    Typography,
    withStyles
} from '@material-ui/core';
import { login } from '../../services/auth';

const useStyles = theme => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
      },
      avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.error.main,
      },
      form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(1),
      },
      submit: {
        margin: theme.spacing(3, 0, 2),
      },
});
  

class Login extends React.Component {
    constructor(props) {
        super(props)

        this.state = {
            email: '',
            emailErrorText: '',
            senha: '',
            senhaErrorText: '',
        }
    }

    isValidEmail = () => {
        return this.state.emailErrorText.trim().length === 0;
    }

    isValidSenha = () => {
        return this.state.senhaErrorText.trim().length === 0;
    }

    handleChangeEmail = (e) => {
        this.setState({ email: e.target.value })
    }

    handleChangeSenha = (e) => {
        this.setState({ senha: e.target.value })
    }

    validateEmail = (e) => {
        var emailErrorText = ''
        if (e.target.value.trim().length === 0)
            emailErrorText = 'Campo obrigatório'
        else if (!e.target.validity.valid) {
            emailErrorText = 'Insira um formato válido de e-mail'
        }

        this.setState({ emailErrorText })
    }

    validateSenha = (e) => {
        var senhaErrorText = ''
        if (e.target.value.trim().length === 0)
            senhaErrorText = 'Campo obrigatório'

        this.setState({ senhaErrorText })
    }

    handleLogin = (e) => {
        e.preventDefault();
        
        const { email, senha } = this.state;
        const options = {
            headers: { 'Content-Type': 'application/json' }
        }
        api.post('auth', { email, senha }, options)
            .then(res => {
                if (res.status === 200) {
                    login(res.data.jwt)
                    const { history } = this.props;
                    history.push('/')
                }
            })
    }
    
    render() {
        const {classes} = this.props;

        return (
            <Container
                component="main"
                maxWidth="xs"
                className={classes.root}
            >
                <CssBaseline />
                
                <div className={ classes.paper }>
                    <Avatar className={ classes.avatar }>
                        <LockOutlined  />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Sign in
                    </Typography>
                    <form className={ classes.form } noValidate onSubmit={this.handleLogin}>
                        <TextField
                            type="email"
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            id="email"
                            label="Email Address"
                            name="email"
                            autoComplete="email"
                            autoFocus
                            error={!this.isValidEmail()}
                            helperText={this.state.emailErrorText}
                            onChange={this.handleChangeEmail}
                            onBlur={this.validateEmail}
                            />
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                            error={!this.isValidSenha()}
                            helperText={this.state.senhaErrorText}
                            onChange={this.handleChangeSenha}
                            onBlur={this.validateSenha}
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={ classes.submit }
                            disabled={!this.isValidEmail() || !this.isValidSenha()}
                        >
                            Sign In
                        </Button>
                    </form>
                </div>
            </Container>
        )
    }
}

export default withStyles(useStyles)(Login)