import React from 'react';
import {ajax} from 'jquery';
import ReactList from 'react-list'

export class Home extends React.Component {
  constructor() {
    super();
    this.renderItem = this.renderItem.bind(this);
    this.state = {
      repos: [] // TODO: use local forage to pull the last good state (then back-fill with API)
    };
  }

  renderItem(index, key) {
    var repo = this.state.repos[index];
    return (
      <div className="col-md-12">
        <div key={key} className="panel panel-default">
          <div className="panel-heading">
            <h3 className="panel-title">{repo.FullName}</h3>
          </div>
          <div className="panel-body">
            {repo.Description}
          </div>
        </div>
      </div>
    );
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
        <h2>Your Repos?</h2>
        <div>
          <ReactList itemRenderer={this.renderItem} length={this.state.repos.length} type='simple' />
        </div>
      </div>
    );
  }
}
