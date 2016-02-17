import React from 'react';
import {Route, HashLocation, Router} from 'react-router';
import WelcomeIndex from './concerns/welcome';
import Master from './components/master';

export default class AppRouter {
  static initializeApplicationRoutes() {
    React.render((
      <Router>
        <Route path="/" component={Master}>
          <Route path="welcome" component={WelcomeIndex} />
        </Route>
      </Router>
    ), document.body);
  }
}
