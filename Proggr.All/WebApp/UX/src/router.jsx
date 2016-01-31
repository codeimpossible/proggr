import React from 'react';
import {Route, HashLocation, Router} from 'react-router';
import Profile from './components/sections/profile';
import AdminIndex from './concerns/admin';
import JobsIndex from './concerns/admin/jobs';
import WelcomeIndex from './concerns/welcome';
import Master from './components/master';

export default class AppRouter {
  static initializeApplicationRoutes() {
    React.render((
      <Router>
        <Route path="/" component={Master}>
          <Route path="welcome" component={WelcomeIndex} />
          <Route path="profile" component={Profile} />
          <Route path="admin" component={AdminIndex}>
            <Route path="jobs" component={JobsIndex} />
          </Route>
        </Route>
      </Router>
    ), document.body);
  }
}
