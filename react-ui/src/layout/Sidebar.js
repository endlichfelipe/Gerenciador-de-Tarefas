import React from 'react';
import { 
    Divider, 
    Drawer, 
    List, 
    ListItem, 
    ListItemText, 
    withStyles 
} from '@material-ui/core';
import CreateIcon from '@material-ui/icons/Create';
import ListIcon from '@material-ui/icons/List';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import { Link } from 'react-router-dom';

const drawerWidth = 240;

const useStyles = theme => ({
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
  },
  drawerPaper: {
    width: drawerWidth,
  },
  // necessary for content to be below app bar
  toolbar: theme.mixins.toolbar,
});

class Sidebar extends React.Component {
    render() {
        const { classes } = this.props;
        
        return(
            <Drawer
                className={classes.drawer}
                variant="permanent"
                classes={{
                paper: classes.drawerPaper,
                }}
                anchor="left"
            >
                <div className={classes.toolbar} />
                <Divider />
                <List>
                    <ListItem button component={ Link } to="/tarefas">
                        <ListItemIcon><ListIcon /></ListItemIcon>
                        <ListItemText primary="Lista de Tarefas" />
                    </ListItem>
                    <ListItem button component={ Link } to="/tarefa/nova">
                        <ListItemIcon><CreateIcon /></ListItemIcon>
                        <ListItemText primary="Nova Tarefa" />
                    </ListItem>
                </List>
            </Drawer>
        )
    }
}

export default withStyles(useStyles)(Sidebar)