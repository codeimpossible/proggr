import React from 'react';
import {Link} from 'react-router'

export default class WelcomeIndex extends React.Component {
  render() {
    return (
      <div className="row">
        <h2>Welcome</h2>
      </div>
    );
  }
}

WelcomeIndex.propTypes = {
  currentUser: React.PropTypes.object.isRequired
};
