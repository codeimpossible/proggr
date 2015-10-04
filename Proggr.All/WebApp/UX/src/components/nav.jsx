import React from 'react';
import {Link} from 'react-router'

import {Wait} from './wait';

export class Nav extends React.Component {
  render() {
    let profileNav = this.props.currentUser ? (
      <Link to="/profile">
        <img width="24" height="24" className="avatar" src={this.props.currentUser.avatar_url} />
        {this.props.currentUser.login}
      </Link>
    ) : (
      <Wait />
    );
    return (
      <nav className="navbar navbar-inverse navbar-fixed-top">
        <div className="container">
          <div className="navbar-header">
            <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
              <span className="icon-bar"></span>
              <span className="icon-bar"></span>
              <span className="icon-bar"></span>
            </button>
            <Link to="/" className="navbar-brand">proggr</Link>
          </div>
          <div className="navbar-collapse collapse">
            <ul className="nav navbar-nav">
              <li><Link to="/">Home</Link></li>
            </ul>
            <ul className="nav navbar-nav navbar-right">
              <li>
                {profileNav}
              </li>
            </ul>
          </div>
        </div>
      </nav>
    );
  }
}
