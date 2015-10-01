import React from 'react';
import {Link} from 'react-router'

export class Nav extends React.Component {
  render() {
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
              <li><Link to="/profile">Your Profile</Link></li>
            </ul>
          </div>
        </div>
      </nav>
    );
  }
}
