import React from 'react';

export class List extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div className="row">
        <div clasName="row">
          <input type="text" className="form-control input-lg" placeholder="Search Repositories" />
        </div>
        <div className="row">
          
        </div>
      </div>
    );
  }
}