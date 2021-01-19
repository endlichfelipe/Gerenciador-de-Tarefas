import React from 'react';
import Login from './features/login/Login'
import Tarefas from './features/tarefas/Tarefas'
import NovaTarefa from './features/tarefas/NovaTarefa'
import './App.css';
import { createMuiTheme, ThemeProvider } from '@material-ui/core';
import { blue, green, red } from '@material-ui/core/colors';
import { Redirect, Route, Switch } from 'react-router-dom';
import EditTarefa from './features/tarefas/EditTarefa';
import { isAuthenticated } from './services/auth';

const theme = createMuiTheme({
  spacing: 8,
  palette: {
    primary: {
      main: blue[700],
    },
    secondary: {
      main: green[500],
    },
    error: {
      main: red[600]
    }
  },
})

const PrivateRoute = ({ component: Component, ...rest }) => (
    <Route
      {...rest}
      render={props => isAuthenticated() ? (
        <Component {...props} />
      ) : (
        <Redirect to={{ pathname: "/login" }} />
      )}
    />
)

class Routes extends React.Component {
  render() {
    return(
      <Switch>
        <Route path="/login" exact component={Login} />
        <Redirect exact from="/" to="/tarefas"  />
        <PrivateRoute path="/tarefas" exact component={Tarefas} />
        <PrivateRoute path="/tarefa/nova" exact component={NovaTarefa} />
        <PrivateRoute path="/tarefa/edit/:id" exact component={(props) => <EditTarefa {...props} />} />
      </Switch>
    )
  }
}

class App extends React.Component {
  render () {
    return (
      <ThemeProvider theme={theme}>
        <div className="App">
          <Routes />
        </div>
      </ThemeProvider>
    );
  }
}

export default App;
