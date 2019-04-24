import React, { Component } from 'react';

export class UploadFile extends Component {
    constructor(props) {
        super(props);
        this.state = { files: null }

        this.handleFileChange = this.handleFileChange.bind(this);
        this._handleSubmit = this._handleSubmit.bind(this);
    }

    _handleSubmit(event) {
        event.preventDefault();
        event.stopPropagation();
        let formData = new FormData();
        for (let i = 0; i < this.state.files.length; i++) {
            formData.append('files', this.state.files[i]);
        }
        fetch('/api/files', {
            method: 'POST',
            body: formData
        }).then();
        return false;
    }

    handleFileChange(event) {
        this.setState({ files: event.target.files });
    }

    render() {
        return (
            <form>
                <input type="file" multiple={true} name="pic" onChange={this.handleFileChange} />
                <input type="submit" onClick={this._handleSubmit} />
            </form>
        );
    }
}

export default UploadFile;