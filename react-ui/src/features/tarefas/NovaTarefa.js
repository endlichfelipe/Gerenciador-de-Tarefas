import React from 'react';
import { 
    withStyles
} from '@material-ui/core';
import Dashboard from '../../layout/Dashboard';

const useStyles = theme => ({
});
  

class NovaTarefa extends React.Component {
    render() {
        return (
            <Dashboard>
                <div>NovaTarefa</div>
            </Dashboard>
        )
    }
}

export default withStyles(useStyles)(NovaTarefa)