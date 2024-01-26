import { Button, Card, CardFooter, Col, Container, Row } from "react-bootstrap";
import { ChevronBarRight, Pencil } from "react-bootstrap-icons";
import postService from "../services/posts.service";
import { NewPost, Post, PostFormProps } from "../types/types";
import React from "react";

const PostForm = ({ onSubmit }: PostFormProps) => {
    const [post, setPost] = React.useState<NewPost>({ title: "", body: ""});

    function setTitle(title: string) {
        setPost({ ...post, title: title });
    }

    function setBody(body: string) {
        setPost({ ...post, body: body });
    }

    async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget;
        const formData = new FormData(form);

        try {
            const post: Post = await postService.createPost(formData);
            setBody("");
            setTitle("");
            onSubmit(post);
        } catch (e) {
            console.error(e);
        }
    }

    return (
        <Container className="mt-5">
            <Row>
                <Col sm={2}>
                </Col>
                <Col sm={10}>
                    <form method="post" onSubmit={handleSubmit}>
                        <h5><Pencil/> Write a post</h5>
                        <Card className="shadow">
                            <input type="text" name="title" placeholder="Title" value={post.title} onChange={e => setTitle(e.target.value)}></input>
                            <textarea name="body" placeholder="Share your thoughts..." value={post.body} onChange={e => setBody(e.target.value)}></textarea>
                            <div className="images">
                            </div>
                            <CardFooter className="text-muted">
                                <Button type="submit" variant="primary" disabled={!post.body}>
                                    Share <ChevronBarRight />
                                </Button>
                            </CardFooter>
                        </Card>
                    </form>
                </Col>
            </Row>
        </Container>
    );
}

export default PostForm;