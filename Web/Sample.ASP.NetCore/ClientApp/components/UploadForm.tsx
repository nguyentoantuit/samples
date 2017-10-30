import * as React from 'react';
import * as Dropzone from 'react-dropzone';
import * as stylesImport from './styles.scss';
const styles: any = stylesImport;

interface UploadFormProps {

}

interface UploadFormState {

}

class UploadForm extends React.Component<UploadFormProps, UploadFormState> {
    private _filePicker: Dropzone;

    constructor(props: UploadFormProps, context) {
        super(props, context);
        this._onDrop = this._onDrop.bind(this);
    }

    componentDidMount() {
    }

    componentWillReceiveProps(nextProps: UploadFormProps, nextContext?) {
    }

    private _onDrop(files: File[], rejected) {
        if (files && files.length > 0) {
            var name = files[0].name.split('.')[0];
            console.log(`File name: ${name}`);
        }
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
                <div className="form-group">
                    <label htmlFor="usr">Name:</label>
                    <input type="text" className="form-control" id="usr" />
                </div>
            </Dropzone>
        );
    }
}

export default UploadForm;