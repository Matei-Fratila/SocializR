import { Button, Card, CardBody, CardFooter, CardHeader, Col, Container, Row } from "react-bootstrap";
import { PostProps } from "../types/types";
import { Link } from "react-router-dom";
import { Heart, Chat, HeartFill } from "react-bootstrap-icons";
import "./Post.css";

const Post = ({ item }: PostProps) => (
    <Container className="mt-5">
        <Row>
            <Col sm={2}>
                <Link to={`/profile`}></Link>
            </Col>
            <Col sm={8} className="card-container">
                <Card className="shadow">
                    <CardHeader>
                        <Link to={`/profile`}>{item.firstName} {item.lastName}</Link>
                        <span className="float-right">{ }</span>
                    </CardHeader>
                    <CardBody>
                        <h5 className="card-title">{item.title}</h5>
                        <p className="card-text pre search-for-link">{item.body}</p>
                    </CardBody>
                    <div className="images">
                    </div>
                    <CardFooter className="text-muted post-footer">
                        <Button variant="light" className={item.isLikedByCurrentUser ? 'liked' : 'like'} 
                            title={item.isLikedByCurrentUser ? "click to unlike" : "click to like"} data-toggle="tooltip" data-placement="bottom">
                            <HeartFill />
                        </Button>

                        <Button variant="light" className="float-end" title="see comments" data-toggle="tooltip" data-placement="bottom">
                            <Chat /> <span className="nr-of-comments">{item.numberOfComments} Comments</span>
                        </Button>

                        <Button variant="light" className="float-end" title="see likes" data-toggle="tooltip" data-placement="bottom">
                            <Heart /> <span className="nr-of-likes see-likes">{item.numberOfLikes} Likes</span>
                        </Button>

                        <div className="comments-container">
                            <div className="fresh-comments-container mt-3"></div>
                            <a className="btn see-comments more-comments" title="see more comments" data-toggle="tooltip" data-placement="bottom">
                                <i className="fa fa-plus"></i><span className="nr-of-likes">Load more comments</span>
                            </a>

                            <div className="insert-container comment-container mt-3 mb-3">
                                <div className="card panel-default">
                                    <input asp-for="@Model.Id" value="@Model.Id" name="@Model.Id" type="hidden" className="post-id" />
                                    <textarea className="card-body comment-body text"></textarea>
                                    <div className="card-footer">
                                        <button className="btn btn-primary comment">
                                            Comment <i className="fa fa-chevron-right"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </CardFooter>
                </Card>
            </Col>
        </Row>
    </Container>
);

export default Post;