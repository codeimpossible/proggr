import React from 'react';
import {Link} from 'react-router'

import {Wait} from './wait'

import '../styles/sidebar.css'

export class Sidebar extends React.Component {
  render() {
    let profileInfo = this.props.currentUser ? (
      <div className="col-md-12 profile-container">
        <div className="avatar">
          <img src={this.props.currentUser.avatar_url} className="img-responsive" />
        </div>
        <div className="user-title">
          <div className="name">{this.props.currentUser.name}</div>
        </div>
      </div>
    ) : (
      <Wait />
    );
    return (
      <div className="col-md-3 sidebar">
        <div className="row profile">
          {profileInfo}
        </div>
        <div className="row">
          <div className="col-md-12">
            <div className="well">
              <h2>Context Sensitive Links</h2>
            </div>
          </div>
        </div>
      </div>
    );
  }
}
