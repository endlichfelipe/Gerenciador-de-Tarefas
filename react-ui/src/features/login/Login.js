import React from 'react';
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
    authenticated = 'teste';
    
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
                    <form className={ classes.form } noValidate>
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            id="email"
                            label="Email Address"
                            name="email"
                            autoComplete="email"
                            autoFocus
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
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={ classes.submit }
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