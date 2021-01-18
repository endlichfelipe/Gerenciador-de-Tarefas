import React from 'react';
import Login from './features/login/Login'
import './App.css';
import { createMuiTheme, ThemeProvider } from '@material-ui/core';
import { blue, green, red } from '@material-ui/core/colors';

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

class App extends React.Component {
  render () {
    return (
      <ThemeProvider theme={theme}>
        <div className="App">
          <Login />
        </div>
      </ThemeProvider>
    );
  }
}

export default App;
