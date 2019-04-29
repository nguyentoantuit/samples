import React, { Component, Fragment } from 'react';

export class UploadFile extends Component {
    constructor(props) {
        super(props);
        this.state = { files: null, result: null }

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

        let url = event.target.value === 'Use Binding' ? '/api/files/binding' : '/api/files/streaming';

        fetch(url, {
            method: 'POST',
            body: formData
        }).then(r => r.json()).then(result => {

            let state = { ...this.state };
            state.result = result;
            this.setState(state);
        });
        return false;
    }

    handleFileChange(event) {
        this.setState({ files: event.target.files });
    }

    render() {
        return (
            <Fragment>
                <form>
                    <div><input type="file" multiple={true} name="pic" onChange={this.handleFileChange} /></div>
                    <br />
                    <div><input type="submit" value="Use Binding" onClick={this._handleSubmit} /></div>
                    <br />
                    <div><input type="submit" value="Use Streaming" onClick={this._handleSubmit} /></div>
                </form>
                <div>
                    {this.state.result && this.state.result.split("\r\n").map(x => <p>{x}</p>)}
                </div>
            </Fragment>
        );
    }
}

export default UploadFile;