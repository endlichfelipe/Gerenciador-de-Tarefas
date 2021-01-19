import React from 'react';
import { 
    AppBar, 
    Button,
    Toolbar, 
    Typography, 
    withStyles 
} from '@material-ui/core';
import { logout } from '../services/auth';

const drawerWidth = 240;

const useStyles = theme => ({
  appBar: {
    width: `calc(100% - ${drawerWidth}px)`,
    marginLeft: drawerWidth,
  },
  title: {
    textAlign: 'left',
    flexGrow: 1
  }
});

class Navbar extends React.Component {
    handleLogout = () => {
        logout();
        window.location.reload();
    }

    render() {
        const { classes } = this.props;
        
        return(
            <AppBar position="static" className={ classes.appBar }>
                <Toolbar>
                    <Typography variant="h6" noWrap className={ classes.title }>
                        Gerenciador de Tarefas
                    </Typography>
                    <Button color="inherit" onClick={this.handleLogout}>Sair</Button>
                </Toolbar>
            </AppBar>
        )
    }
}

export default withStyles(useStyles)(Navbar)