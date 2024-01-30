import { Button, Card, CardBody, CardFooter, CardGroup, CardHeader, CardText, CardTitle, Col } from "react-bootstrap";
import authService from "../services/auth.service";
import { Link, useParams } from "react-router-dom";
import React from "react";
import albumService from "../services/album.service";
import { Media, MediaType } from "../types/types";

const EditMedia = () => {
    const { id } = useParams();
    const [media, setMedia] = React.useState({ id : "", caption : "", type : MediaType.Unspecified, createdDate : "", fileName : ""});

    const handleFetchGallery = React.useCallback(async () => {
        try {
            if (id !== undefined) {
                const mediaResponse = await albumService.getMedia(id);
                setMedia(mediaResponse);
            }
        }
        catch {
        }
    }, [id]);

    React.useEffect(() => {
        handleFetchGallery();
    }, [id]);

    return (
        <CardGroup>
                <Card>
                    {media.type === MediaType.Image && <img src={`/api/${media.fileName}`} className="card-img-top" alt="..."></img>}
                    {media.type === MediaType.Video && <video controls src={`/api/${media.fileName}`} className="card-img-top"></video>}
                    <CardBody>
                        <CardText>{media.caption}</CardText>
                        <CardText><small className="text-muted">{media.createdDate}</small></CardText>
                    </CardBody>
                    <CardFooter>
                        <Button>Save changes</Button>
                    </CardFooter>
                </Card>
        </CardGroup>
    );
};

export default EditMedia;