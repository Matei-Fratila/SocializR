import { Button, Card, CardBody, CardFooter, CardGroup, CardHeader, CardText, CardTitle, Col } from "react-bootstrap";
import authService from "../services/auth.service";
import { Pencil, Trash } from "react-bootstrap-icons";
import { Link, useParams } from "react-router-dom";
import React from "react";
import albumService from "../services/album.service";
import { Media, MediaType } from "../types/types";

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
            await albumService.deleteAlbum(id);
            setAlbum({ ...album, media: album.media.filter(m => m.id !== id) });
        } catch {

        }
    };

    return (
        <CardGroup>
            {album.media.map((media: Media) => (
                <Card>
                    {media.type === MediaType.Image && <img src={`/api/${media.fileName}`} className="card-img-top" alt="..."></img>}
                    {media.type === MediaType.Video && <video controls src={`/api/${media.fileName}`} className="card-img-top"></video>}
                    <CardBody>
                        <CardText>{media.caption}</CardText>
                        <CardText><small className="text-muted">{media.createdDate}</small></CardText>
                    </CardBody>
                    <CardFooter>
                        <Button variant="link">
                            <Link to={`/album/gallery/media/${media.id}`}>
                                <Pencil />
                            </Link>
                        </Button>
                        <Button variant="link" className="float-end" onClick={() => handleDelete(media.id)}>
                            <Trash />
                        </Button>
                    </CardFooter>
                </Card>
            ))}
        </CardGroup>
    );
};

export default Gallery;