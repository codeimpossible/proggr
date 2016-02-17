import React from 'react';
import {ajax} from 'jquery';

export default class Profile extends React.Component {
  constructor() {
    super();
    this.state = {
      user: {}
    };
  }

  componentDidMount() {
    ajax('/api/user').then(function(user) {
      this.setState({
        user: user
      });
    }.bind(this));
  }

  render() {
    return (
      <div className="col-md-12">
        <div className="col-md-4">
          <img src={this.state.user.avatar_url} height="256" width="256"/>
          <h2>{this.state.user.login}</h2>
        </div>
        <div className="col-md-8">

        </div>
      </div>
    );
  }
}
