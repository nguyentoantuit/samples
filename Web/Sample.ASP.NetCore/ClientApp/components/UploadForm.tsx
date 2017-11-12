import * as React from 'react';
import * as Dropzone from 'react-dropzone';
import * as stylesImport from './styles.scss';
const styles: any = stylesImport;

interface UploadFormProps {
}

interface UploadFormState {
    fileName: string;
}

class UploadForm extends React.Component<UploadFormProps, UploadFormState> {
    private _filePicker: Dropzone;

    constructor(props: UploadFormProps, context) {
        super(props, context);
        this._onDrop = this._onDrop.bind(this);
        this._openFileSelectionDialog = this._openFileSelectionDialog.bind(this);
        this.state = {
            fileName: ''
        };
    }

    private _onDrop(files: File[], rejected) {
        if (files && files.length > 0) {
            this.setState({ fileName: files[0].name});
            alert(`File name: ${files[0].name}`);
        }
    }

    private _openFileSelectionDialog() {
        this._filePicker.open();
    }

    render() {
        return (
            <Dropzone
                ref={ref => this._filePicker = ref}
                accept="application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                className="file-picker"
                rejectClassName="rejected-file"
                acceptClassName="accepted-file"
                onDrop={this._onDrop}
                multiple={false}
                disableClick={true}
            >
                <div className="container">
                    <div className="row">
                        <div className="col col-sm-12 col-md-12 col-lg-12">
                            <label>File:</label>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col col-sm-10 col-md-10 col-lg-10">
                            <input type="text" className="form-control" value={this.state.fileName} onClick={this._openFileSelectionDialog} />
                        </div>
                        <div className="col col-sm-2 col-md-2 col-lg-2">
                            <button type="button" className="btn btn-default" onClick={this._openFileSelectionDialog}>Brown</button>
                        </div>
                    </div>
                </div>
            </Dropzone>
        );
    }
}

export default UploadForm;