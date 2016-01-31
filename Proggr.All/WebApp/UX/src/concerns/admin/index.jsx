import React from 'react';
import {Link} from 'react-router'

export default class AdminIndex extends React.Component {
  render() {
    return (
      <div className="row">
        <h2>Admin Area</h2>
        {this.props.children}
      </div>
    );
  }
}
