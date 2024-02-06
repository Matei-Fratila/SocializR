import { Button, Card, CardBody, CardFooter, CardHeader, CardText, CardTitle, Col } from "react-bootstrap";
import authService from "../services/auth.service";
import { Pencil, Trash } from "react-bootstrap-icons";
import { Link } from "react-router-dom";
import { AlbumProps } from "../types/types";
import albumService from "../services/album.service";
import axios from "axios";

const Album = ({ item, onDelete }: AlbumProps) => {
    const authenticatedUserId = authService.getCurrentUserId();
    const album = item;

    const handleDeleteAlbum = async () => {
        try {
            await albumService.deleteAlbum(album.id);
            onDelete(album);
        } catch (e) {

        }
    }

    return (
        <Col sm={6} className="mb-5">
            <Card className="shadow">
                {
                    authenticatedUserId !== undefined &&
                    <CardHeader>
                        <Button variant="link">
                            <Link to={``}>
                                <Pencil />
                            </Link>
                        </Button>
                        <Button variant="link" className="float-end" onClick={handleDeleteAlbum}>
                            <Trash />
                        </Button>
                    </CardHeader>
                }
                <Link to={`/album/gallery/${album.id}`}>
                    <img src={`${axios.defaults.baseURL}${album.coverFilePath}`} className="card-img-bottom" />
                </Link>
                <CardBody>
                    <CardTitle>{album.name}</CardTitle>
                    <CardText>
                        <p>{album.description}</p>
                    </CardText>
                </CardBody>
                <CardFooter>
                    <small className="text-muted">{album.nrOfImages} photos</small>
                    <small className="text-muted float-end">Last updated {album.createdDate}</small>
                </CardFooter>
            </Card>
        </Col>);
};

export default Album;