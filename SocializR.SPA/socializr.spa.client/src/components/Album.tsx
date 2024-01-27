import { Button, Card, CardBody, CardFooter, CardHeader, CardText, CardTitle, Col } from "react-bootstrap";
import authService from "../services/auth.service";
import { Pencil, Trash } from "react-bootstrap-icons";
import { Link } from "react-router-dom";
import { AlbumProps } from "../types/types";
import React from "react";

const Album = ({item}: AlbumProps) => {
    const authenticatedUserId = authService.getCurrentUserId();
    const [album, setAlbum] = React.useState(item);

    return (
        <Col>
            <Card className="shadow">
                {
                    authenticatedUserId !== undefined &&
                    <CardHeader>
                        <Link to={``}>
                            <Pencil/>
                        </Link>
                        <Button variant="link" className="float-end">
                            <Trash/>
                        </Button>
                    </CardHeader>
                }
                <Link to={`Album/Details/${1}`}>
                    <img src={`/api/${album.coverFilePath}`} className="card-img-bottom"/>
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