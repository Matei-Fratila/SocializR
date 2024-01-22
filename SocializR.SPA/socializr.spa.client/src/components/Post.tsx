import { Card, CardBody, CardFooter, CardHeader, Col, Container, Row } from "react-bootstrap";

const Post = () => (<>
    <Container className="mt-5">
        <Row>
            <Col sm={2}>
                <a asp-controller="Profile" asp-action="Index" asp-route-id="@Model.UserId"></a>
            </Col>
            <Col sm={8} className="card-container">
                <Card className="shadow">
                    <CardHeader>
                        <a asp-controller="Profile" asp-action="Index" asp-route-id="@Model.UserId" className="text-bold">@Model.FirstName @Model.LastName</a>
                        @if (Model.UserId == currentUser.Id)
                        {
                            <a className="float-right ms-2 delete-post-btn"><i className="fa fa-trash"></i></a>
                        }
                        <span className="float-right">@Model.CreatedOn</span>
                    </CardHeader>
                    <CardBody>
                        <h5 className="card-title">@Model.Title</h5>
                        <p className="card-text pre search-for-link">@Model.Body</p>
                    </CardBody>
                    <div className="images">
                    </div>
                    <CardFooter className="text-muted post-footer">
                        @*Post Buttons*@
                        <Row>
                            <input type="hidden" className="comments-page" />
                            <input asp-for="@Model.Id" value="@Model.Id" name="@Model.Id" type="hidden" className="post-id" />
                            <input asp-for="@Model.IsLikedByCurrentUser" value="@Model.IsLikedByCurrentUser" name="@Model.IsLikedByCurrentUser" type="hidden" className="is-liked" />

                            <button className="col btn like" title="heart post" data-toggle="tooltip" data-placement="bottom">
                                <i className="fa fa-heart"></i>
                            </button>
                            <button className="col btn" title="see likes" data-toggle="tooltip" data-placement="bottom">
                                <i className="fa fa-heart"></i><span className="nr-of-likes see-likes">@Model.NumberOfLikes Hearts</span>
                            </button>
                            <button className="col btn see-comments" title="nr. of comments" data-toggle="tooltip" data-placement="bottom">
                                <i className="fa fa-comments"></i><span className="nr-of-comments">@Model.NumberOfComments Comments</span>
                            </button>
                        </Row>

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
</>);

export default Post;