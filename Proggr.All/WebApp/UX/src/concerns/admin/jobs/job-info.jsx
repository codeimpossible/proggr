import React from 'react';
import {Link} from 'react-router'

export default class JobInfo extends React.Component {

  toDefinitionList(object, whitelist) {
    for(var p in object) {
      if(object.hasOwnProperty(p) && (!whitelist || (whitelist && ~whitelist.indexOf(p)))) {
        return (
          <dl>
            <dt>{p}:</dt>
            <dd>{object[p]}</dd>
          </dl>
        );
      }
    }
  }

  small() {
    console.log(this.props.job);
    return (
      <dl>
        <dt>Id:</dt>
        <dd>{this.props.job.Id}</dd>

        <dt>Arguments:</dt>
        <dd>{this.toDefinitionList(JSON.parse(this.props.job.Arguments  || {}))}</dd>

        <dt>Date Created:</dt>
        <dd>{this.props.job.DateCreated}</dd>

        <dt>Status:</dt>
        <dd>{this.props.job.Status}</dd>
      </dl>
    );
  }

  render() {
    return (
      <div className="row">
        {this[this.props.style]()}
      </div>
    );
  }
}
JobInfo.propTypes = { job: React.PropTypes.object.isRequired, style: React.PropTypes.string };
JobInfo.defaultProps = { style: 'small' };
