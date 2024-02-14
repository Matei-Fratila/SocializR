import { Button, Card, CardBody, CardFooter, CardText } from "react-bootstrap";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import React from "react";
import albumService from "../services/album.service";
import { Media as MediaModel, MediaType } from "../types/types";
import axios from "axios";
import { Pencil, Trash } from "react-bootstrap-icons";
import authService from "../services/auth.service";

const Media = () => {
    const navigate = useNavigate();
    const location = useLocation()
    const { id } = useParams();
    const [media, setMedia] = React.useState<MediaModel>(
        {
            id: "",
            albumId: "",
            caption: "",
            type: MediaType.Unspecified,
            createdDate: new Date(),
            createdDateDisplay: "",
            lastModifiedDate: new Date(),
            lastModifiedDateDisplay: "",
            fileName: ""
        });

    const handleFetchMedia = React.useCallback(async () => {
        try {
            if (id !== undefined) {
                const mediaResponse: MediaModel = await albumService.getMedia(id);

                setMedia(mediaResponse);
            }
        }
        catch {
        }
    }, [id]);

    React.useEffect(() => {
        handleFetchMedia();
    }, [id]);

    return (
        <Card>
        {media.type === MediaType.Image && <img src={`${axios.defaults.baseURL}${media.fileName}`} className="card-img-top" alt="..."></img>}
        {media.type === MediaType.Video && <video controls src={`${axios.defaults.baseURL}${media.fileName}`} className="card-img-top"></video>}
        <CardBody>
            <CardText>{media.caption}</CardText>
            <CardText>
                <small className="text-muted">Created {media.createdDateDisplay}</small>
                {(media.lastModifiedDateDisplay !== "") && <small className="text-muted float-end">Updated {media.lastModifiedDateDisplay}</small>}
            </CardText>
        </CardBody>
        <CardFooter>
            <Button variant="link">
                <Link to={`/media/edit/${media.id}`}>
                    <Pencil />
                </Link>
            </Button>
            {authService.getCurrentUserPhoto() !== media.fileName &&
            <Button variant="link" className="float-end" onClick={() => {location.state.onDelete(media.id); navigate(`/album/gallery/${media.albumId}`)}}>
                <Trash />
            </Button>}
        </CardFooter>
    </Card>
    );
};

export default Media;