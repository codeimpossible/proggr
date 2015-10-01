import React from 'react';
import {Route, HashLocation, default as Router} from 'react-router';
import {Profile} from './components/sections/profile';
import {Home} from './components/sections/home';
import {Master} from './components/master';

export class AppRouter {
  static initializeRoutes() {
    // declare our routes and their hierarchy
    const routes = (
      <Route handler={Master}>
        <Route name="/" handler={Home}/>
        <Route name="profile" handler={Profile}/>
      </Route>
    );

    Router.run(routes, HashLocation, (Root) => {
      React.render(<Root/>, document.body);
    });
  }
}