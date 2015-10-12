import React from 'react';
import {Link} from 'react-router'
import ReactList from 'react-list'

export class RepoList extends React.Component {
  constructor(props) {
    super(props);
    this.renderItem = this.renderItem.bind(this);
  }

  renderItem(index, key) {
    var repo = this.props.repos[index];
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

  render() {
    return (
      <div className="row">
        <div clasName="row">
          <input type="text" className="form-control input-lg" placeholder="Search Repositories" />
        </div>
        <div className="row">
          <ReactList itemRenderer={this.renderItem} length={this.props.repos.length} type={this.props.type} />
        </div>
      </div>
    );
  }
}
RepoList.propTypes = {
  // TODO: use local forage to pull the last good state (then back-fill with API)
  repos: React.PropTypes.array
};
RepoList.defaultProps = {
  repos: []
};
