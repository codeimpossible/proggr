import React from 'react';
import {Link} from 'react-router'

import {Wait} from './wait'

import '../styles/sidebar.css'

export class Sidebar extends React.Component {
  render() {
    let profileInfo = this.props.currentUser ? (
      <div>
        <div className="avatar">
          <img src={this.props.currentUser.AvatarUrl} className="img-responsive" />
        </div>
        <div className="user-title">
          <div className="name">{this.props.currentUser.Name}</div>
        </div>
      </div>
    ) : (
      <Wait />
    );
    return (
      <div className="col-md-3 sidebar">
        <div className="row profile">
          <div className="col-md-12 profile-container">
            {profileInfo}
          </div>
        </div>
        <div className="row">
          <div className="col-md-12">
            <ul className="nav nav-pills nav-stacked">
              <li role="presentation"><a href="#">Add Repository</a></li>
              <li role="presentation"><a href="#">Profile</a></li>
              <li role="presentation"><a href="#">Messages</a></li>
            </ul>
          </div>
        </div>
      </div>
    );
  }
}
