import React from 'react';
import {Link} from 'react-router'

import ActivityFeed from '../../components/activityfeed';

export default class WelcomeIndex extends React.Component {
  render() {
    return (
      <div className="row">
        <h2>Welcome</h2>
        <ActivityFeed />
      </div>
    );
  }
}

WelcomeIndex.propTypes = {
  currentUser: React.PropTypes.object.isRequired
};
