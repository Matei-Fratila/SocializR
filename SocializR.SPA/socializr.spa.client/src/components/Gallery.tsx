import { Button, Card, CardBody, CardFooter, CardGroup, CardText, Col, Row } from "react-bootstrap";
import authService from "../services/auth.service";
import { Images, Pencil, Trash } from "react-bootstrap-icons";
import { Link, useParams } from "react-router-dom";
import React from "react";
import albumService from "../services/album.service";
import { Media, MediaType } from "../types/types";
import axios from "axios";

const Gallery = () => {
    const { id } = useParams();
    const [album, setAlbum] = React.useState({
        id: "",
        userId: "",
        name: "",
        description: "",
        coverId: "",
        coverFilePath: "",
        nrOfImages: 0,
        createdDate: "",
        media: [] as Media[]
    });

    const handleFetchGallery = React.useCallback(async () => {
        try {
            if (id !== undefined) {
                const albumResponse = await albumService.getAlbum(id);
                setAlbum(albumResponse);
            }
        }
        catch {
        }
    }, [id]);

    React.useEffect(() => {
        handleFetchGallery();
    }, [id]);

    const handleDelete = async (id: string) => {
        try {
            await albumService.deleteMedia(id);
            setAlbum({ ...album, media: album.media.filter(m => m.id !== id) });
        } catch {

        }
    };

    return (
        <>
            <h5><Images/> {album.name}</h5>
            <hr/>
            <CardGroup>
                <Row>
                    {album.media.map((media: Media) => (
                        <Col sm={6} lg={4} className="mb-3" key={media.id}>
                            <Card>
                                {media.type === MediaType.Image && <img src={`${axios.defaults.baseURL}${media.fileName}`} className="card-img-top" alt="..."></img>}
                                {media.type === MediaType.Video && <video controls src={`${axios.defaults.baseURL}${media.fileName}`} className="card-img-top"></video>}
                                <CardBody>
                                    <CardText>{media.caption}</CardText>
                                    <CardText><small className="text-muted">{media.createdDate}</small></CardText>
                                </CardBody>
                                <CardFooter>
                                    <Button variant="link">
                                        <Link to={`/album/gallery/media/${media.id}`} state={{ isCoverPhoto: media.id === album.coverId }}>
                                            <Pencil />
                                        </Link>
                                    </Button>
                                    {authService.getCurrentUserPhoto() !== media.fileName &&
                                    <Button variant="link" className="float-end" onClick={() => handleDelete(media.id)}>
                                        <Trash />
                                    </Button>}
                                </CardFooter>
                            </Card>
                        </Col>
                    ))}
                </Row>
            </CardGroup>
        </>
    );
};

export default Gallery;