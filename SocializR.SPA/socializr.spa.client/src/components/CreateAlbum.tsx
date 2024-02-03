import { Button, Card, CardBody, CardFooter, CardHeader, Col, Row } from "react-bootstrap";
import authService from "../services/auth.service";
import React from "react";
import albumService from "../services/album.service";
import { useNavigate } from "react-router-dom";
import { Image } from "react-bootstrap-icons";

const CreateAlbum = () => {
    const navigate = useNavigate();
    const authenticatedUserId = authService.getCurrentUserId();
    const [albumName, setAlbumName] = React.useState("");
    const [files, setFiles] = React.useState<File[]>([]);
    const [captions, setCaptions] = React.useState<string[]>([]);

    async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget;
        const formData = new FormData(form);
        console.log(captions);

        try {
            await albumService.createAlbum(formData);
            navigate(`/profile/${authenticatedUserId}`);
        } catch (e) {
            console.error(e);
        }
    }

    return (
        <>
            <h5><Image/> Create album</h5>
            <Card className="shadow">
                <form method="post" onSubmit={handleSubmit}>
                    <CardHeader>
                        <input className="form-control" type="text" name="name" placeholder="Album name" value={albumName}
                            onChange={e => setAlbumName(e.target.value)}>
                        </input>
                    </CardHeader>
                    <CardBody>
                        <Row>
                            {files.length > 0 && Array.from(files).map((file, index) => (
                                <Col sm={6} key={file.name}>
                                    <Card>
                                        {file.type.match('image.*') && <img alt="not found" className="card-img-bottom" src={URL.createObjectURL(file)} />}
                                        {file.type.match('video.*') && <video controls className="card-img-bottom" src={URL.createObjectURL(file)} />}
                                        <textarea className="card-body" name={`captions[${index}]`} value={captions[index]}
                                            placeholder="Image description" onChange={e => {
                                                setCaptions(prevState => prevState.map((caption, i) => i === index ? e.target.value : caption));
                                            }} />
                                    </Card>
                                </Col>
                            ))}
                        </Row>
                    </CardBody>
                    <input className="form-control" type="file" accept="image/*, video/*" multiple name="files"
                        onChange={(e) => {
                            setFiles(Array.from(e.target.files ?? []));
                            setCaptions(Array.from(e.target.files ?? []).map((file, index) => ""));
                        }
                        }
                    />
                    <CardFooter className="text-muted">
                        <Button type="submit" variant="primary" disabled={!albumName}>
                            Create album
                        </Button>
                    </CardFooter>
                </form>
            </Card>
        </>
    );
};

export default CreateAlbum;