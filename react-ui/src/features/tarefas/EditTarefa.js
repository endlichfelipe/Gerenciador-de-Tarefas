import React from 'react';
import { 
    withStyles
} from '@material-ui/core';
import Dashboard from '../../layout/Dashboard';

const useStyles = theme => ({
});
  

class EditTarefa extends React.Component {
    render() {
        const { match: { params } } = this.props;
        const { id } = params;

        return (
            <Dashboard>
                <div>EditTarefa { id }</div>
            </Dashboard>
        )
    }
}

export default withStyles(useStyles)(EditTarefa)