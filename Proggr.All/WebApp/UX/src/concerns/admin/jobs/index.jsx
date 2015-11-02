import React from 'react'
import {ajax} from 'jquery';

import JobsList from './jobs-list';

export default class JobsIndex extends React.Component {
  constructor() {
    super();
    this.state = {
      jobs: [] // TODO: use local forage to pull the last good state (then back-fill with API)
    };
  }

  componentDidMount() {
    ajax('/api/jobs').then(function(jobs) {
      this.setState({
        jobs: jobs
      });
    }.bind(this));
  }

  render() {
    return (
      <div className="row">
        <div className="col-md-12">
          <h2>Jobs.</h2>
          <JobsList jobs={this.state.jobs} />
        </div>
      </div>
    );
  }
}
