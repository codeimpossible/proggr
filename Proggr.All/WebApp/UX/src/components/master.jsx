import React from 'react';
import Router from 'react-router';
import {ajax} from 'jquery';

import {Nav} from './nav';
import {Sidebar} from './sidebar';
import {Wait} from './wait';

let RouteHandler = Router.RouteHandler;

class Master extends React.Component {
  constructor() {
    super();
    this.state = {
      currentUser: null
    };
  }

  componentDidMount() {
    ajax('/api/user').then((user) => {
      this.setState({
        currentUser: user
      });
    });
  }

  render() {
    return (
      <div id="react">
        <header>
          <Nav currentUser={this.state.currentUser} />
        </header>
        <div className="col-md-12 content container">
          <Sidebar currentUser={this.state.currentUser} />
          <div className="col-md-9">
            <RouteHandler />
          </div>
        </div>
      </div>
    );
  }
}

Master.contextTypes = {
  router: React.PropTypes.func,
};

exports.Master = Master;
