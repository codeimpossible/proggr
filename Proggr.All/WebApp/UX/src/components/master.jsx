import React from 'react';
import Router from 'react-router';

import {Nav} from './nav';

let RouteHandler = Router.RouteHandler;

class Master extends React.Component {
  render() {
    return (
      <div id="react">
        <header>
          <Nav />
        </header>
        <div className="col-md-12 content">
          <RouteHandler />
        </div>
      </div>
    );
  }
}

Master.contextTypes = {
  router: React.PropTypes.func,
};

exports.Master = Master;
