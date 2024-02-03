import { Button, Card, CardBody, CardFooter, CardHeader, CardText, CardTitle, Col, Container, Row } from "react-bootstrap";
import { PostProps, Comment as Comm, Comments, MediaType } from "../types/types";
import { Link } from "react-router-dom";
import { Heart, Chat, HeartFill, Trash } from "react-bootstrap-icons";
import Comment from "./Comment";
import "./Post.css";
import React from "react";
import postsService from "../services/posts.service";
import commentService from "../services/comment.service";
import CommentForm from "./CommentForm";
import authService from "../services/auth.service";

const Post = ({ item, onRemoveItem }: PostProps) => {
    const authenticatedUserId = authService.getCurrentUserId();
    const [isLiked, setIsLiked] = React.useState(item.isLikedByCurrentUser);
    const [comments, setComments] = React.useState(item.comments);
    const [numberOfComments, setNumberOfComments] = React.useState(item.numberOfComments);
    const [numberOfLikes, setNumberOfLikes] = React.useState(item.numberOfLikes);
    const [commentsPageNumber, setCommentsPageNumber] = React.useState(1);

    const handleLike = async () => {
        if (isLiked) {
            try {
                await postsService.dislikePost(item.id);
                setIsLiked(false);
                setNumberOfLikes(numberOfLikes - 1);
            } catch (e) {
                console.error(e);
            }

        } else {
            try {
                await postsService.likePost(item.id);
                setIsLiked(true);
                setNumberOfLikes(numberOfLikes + 1);
            } catch (e) {
                console.error(e);
            }
        }
    }

    const handleDeleteComment = async (id: string) => {
        try {
            await commentService.deleteComment(id);
            setNumberOfComments(numberOfComments - 1);
            setComments(comments.filter(x => x.id !== id))
        } catch (e) {
            console.error(e);
        }
    }

    const handleNewComment = (comment: Comm) => {
        try {
            setNumberOfComments(numberOfComments + 1);
            setComments([...comments, comment]);
        } catch (e) {
            console.error(e);
        }
    }

    const handleLoadMoreComments = async () => {
        try {
            const loadedComments: Comments = await commentService.loadComments(item.id, commentsPageNumber);
            setComments([...comments, ...loadedComments]);
            setCommentsPageNumber(commentsPageNumber + 1);
        } catch (e) {

        }
    }

    return (
        <Container className="mb-5">
            <Row>
                <Col sm={2} xs={12}>
                    <Link to={`/profile/${item.userId}`}>
                        <img src={`/api/${item.userPhoto}`} alt="Profile picture" className="d-block m-auto rounded-circle shadow img-thumbnail user-photo avatar-float" />
                    </Link>
                </Col>
                <Col sm={10} xs={12}>
                    <Card className="shadow">
                        <CardHeader>
                            <Link to={`/profile/${item.userId}`}>{item.firstName} {item.lastName}</Link>
                            {
                                item.userId === authenticatedUserId
                                &&
                                <Button variant="link" className="float-end py-0" title="delete post" data-toggle="tooltip" data-placement="bottom"
                                    onClick={() => onRemoveItem(item.id)}>
                                    <Trash />
                                </Button>
                            }
                            <span className="float-end">{item.createdOn}</span>
                        </CardHeader>
                        <CardBody>
                            <CardTitle>{item.title}</CardTitle>
                            <CardText>{item.body}</CardText>
                        </CardBody>
                        {
                            item.media.map((file) => (
                                <>
                                    {file.type === MediaType.Image && <img alt="not found" className="card-img-bottom" src={`/api/${file.fileName}`} />}
                                    {file.type === MediaType.Video && <video controls className="card-img-bottom" src={`/api/${file.fileName}`} />}
                                </>))
                        }

                        <CardFooter className="text-muted">
                            <Button variant="light" className={isLiked ? 'liked' : 'like'} onClick={handleLike} disabled={authenticatedUserId === undefined}
                                title={isLiked ? "click to unlike" : "click to like"} data-toggle="tooltip" data-placement="bottom">
                                <HeartFill />
                            </Button>

                            <Button variant="light" title="see likes" data-toggle="tooltip" data-placement="bottom">
                                <Heart /> <span>{numberOfLikes} Likes</span>
                            </Button>

                            <Button variant="light" title="see comments" data-toggle="tooltip" data-placement="bottom">
                                <Chat /> <span>{numberOfComments} Comments</span>
                            </Button>

                            {comments.map(comment => (
                                <Comment key={comment.id} item={comment} onRemoveItem={() => handleDeleteComment(comment.id)}></Comment>
                            ))}

                            {(comments.length < item.numberOfComments) &&
                                <Container>
                                    <span className="float-end">{comments.length} out of {item.numberOfComments}</span>
                                    <Button variant="link" className="center" title="load more comments" data-toggle="tooltip" data-placement="bottom" onClick={handleLoadMoreComments}>
                                        <span> Load more comments</span>
                                    </Button>
                                </Container>}

                            {authenticatedUserId && <CommentForm onSubmit={handleNewComment} postId={item.id}></CommentForm>}

                        </CardFooter>
                    </Card>
                </Col>
            </Row>
        </Container>
    );
}

export default Post;