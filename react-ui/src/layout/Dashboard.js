import React from 'react';
import { Box, CssBaseline, withStyles } from '@material-ui/core';
import Sidebar from './Sidebar'
import Navbar from './Navbar'

const drawerWidth = 240;

const useStyles = theme => ({
  root: {
    marginLeft: drawerWidth
  }
});

class Dashboard extends React.Component {
    render() {
        const { classes, children } = this.props;

        return(
            <div>
                <CssBaseline />
                <Navbar />
                <Sidebar />
                <Box
                    mx={8}
                    mt={2}
                >
                  <div className = {classes.root}>
                    {children}
                  </div>
                </Box>
            </div>
        )
    }
}

export default withStyles(useStyles)(Dashboard)