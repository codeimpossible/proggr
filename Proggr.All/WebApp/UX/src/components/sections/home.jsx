import React from 'react';
import {ajax} from 'jquery';
import {RepoList} from './../repos/repolist';

export class Home extends React.Component {
  constructor() {
    super();
    this.state = {
      repos: [] // TODO: use local forage to pull the last good state (then back-fill with API)
    };
  }

  componentDidMount() {
    ajax('/api/user/repos').then(function(repos) {
      this.setState({
        repos: repos
      });
    }.bind(this));
  }

  render() {
    return (
      <div className="well">
        <RepoList repos={this.state.repos} type='simple' />
      </div>
    );
  }
}
