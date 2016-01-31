import React from 'react';
import {Link} from 'react-router'
import ReactList from 'react-list'

import JobInfo from './job-info';

export default class JobsList extends React.Component {
  constructor(props) {
    super(props);
    this.renderItem = this.renderItem.bind(this);
  }

  renderItem(index, key) {
    const job = this.props.jobs[index];
    return (
      <div className="col-md-12">
        <div key={key} className="panel panel-default">
          <div className="panel-heading">
            <h3 className="panel-title">{job.JobType}</h3>
          </div>
          <div className="panel-body">
            <JobInfo job={job} style="small" />
          </div>
        </div>
      </div>
    );
  }

  render() {
    return (
      <div className="row">
        <div className="row">
          <ReactList itemRenderer={this.renderItem} length={this.props.jobs.length} type={this.props.type} />
        </div>
      </div>
    );
  }
}
JobsList.propTypes = {
  // TODO: use local forage to pull the last good state (then back-fill with API)
  jobs: React.PropTypes.array
};
JobsList.defaultProps = {
  jobs: []
};
