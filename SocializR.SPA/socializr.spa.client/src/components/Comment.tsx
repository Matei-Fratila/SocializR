import { Button, Card, CardBody, CardHeader, Col, Container, Row } from "react-bootstrap";
import { CommentProps } from "../types/types";
import { Link } from "react-router-dom";
import { Trash } from "react-bootstrap-icons";
import "./Comment.css";

const Comment = ({ item, onRemoveItem }: CommentProps) => {
    return (
        <Container className="mt-3">
            <Row>
                <Col sm={1}>
                    <Link to={`/profile/${item.userId}`}>
                        <img src={'api/' + item.userPhoto} alt="Profile picture" className="rounded-circle shadow img-thumbnail small-user-photo avatar-float" />
                    </Link>
                </Col>
                <Col sm={11} className="no-padding-left">
                    <Card className="panel-default">
                        <CardHeader>
                            <Link to={`/profile/${item.userId}`}>
                                {item.firstName} {item.lastName}
                            </Link>
                            {
                                item.isCurrentUserComment
                                &&
                                <Button variant="light" className="float-end py-0" title="delete comment" data-toggle="tooltip" data-placement="bottom"
                                    onClick={() => onRemoveItem(item.id)}>
                                    <Trash />
                                </Button>
                            }
                            <span className="text-muted float-end">{item.createdOn}</span>
                        </CardHeader>
                        <CardBody>
                            {item.body}
                        </CardBody>
                    </Card>
                </Col>
            </Row>
        </Container>);
};

export default Comment;