import { Button, Card, CardBody, CardHeader, Container } from "react-bootstrap";
import { CommentProps } from "../types/types";
import { Link } from "react-router-dom";
import { Trash } from "react-bootstrap-icons";
import "./Comment.css";
import authService from "../services/auth.service";

const Comment = ({ item, onRemoveItem }: CommentProps) => {
    const authenticatedUserId = authService.getCurrentUserId();
    
    return (
        <Container className="mt-3">
            <Card className="panel-default">
                <CardHeader>
                    <Link to={`/profile/${item.userId}`}>
                        <img src={`/api/${item.userPhoto}`} alt="Profile picture" className="rounded-circle img-thumbnail small-user-photo avatar-float me-1" />
                        {item.firstName} {item.lastName}
                    </Link>
                    {
                        item.isCurrentUserComment && authenticatedUserId !== undefined
                        &&
                        <Button variant="link" className="float-end py-0" title="delete comment" data-toggle="tooltip" data-placement="bottom"
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
        </Container>);
};

export default Comment;