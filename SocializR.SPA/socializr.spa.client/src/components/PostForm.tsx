import { Button, Card, CardBody, CardFooter, Col, Container, Row } from "react-bootstrap";
import { Camera, ChevronBarRight, Pencil } from "react-bootstrap-icons";
import postService from "../services/posts.service";
import { NewPost, Post, PostFormProps } from "../types/types";
import React from "react";
import { useFilePicker } from "use-file-picker";
import { FileAmountLimitValidator, FileSizeValidator, FileTypeValidator, ImageDimensionsValidator } from "use-file-picker/validators";

const PostForm = ({ onSubmit }: PostFormProps) => {
    const [post, setPost] = React.useState<NewPost>({ title: "", body: "" });

    const { openFilePicker, filesContent, loading, errors } = useFilePicker({
        readAs: 'DataURL',
        accept: 'image/*',
        multiple: true,
        validators: [
            new FileAmountLimitValidator({ max: 1 }),
            new FileTypeValidator(['jpg', 'png', 'jpeg']),
            //new FileSizeValidator({ maxFileSize: 50 * 1024 * 1024 /* 50 MB */ }),
            new ImageDimensionsValidator({
                maxHeight: 900, // in pixels
                maxWidth: 1600,
                minHeight: 600,
                minWidth: 768,
            }),
        ],
    });

    if (loading) {
        return <div>Loading...</div>;
    }

    if (errors.length) {
        return <div>Error...</div>;
    }

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
                        <h5><Pencil /> Write a post</h5>
                        <Card className="shadow">
                            <input type="text" name="title" placeholder="Title" value={post.title} onChange={e => setTitle(e.target.value)}></input>
                            <textarea name="body" placeholder="Share your thoughts..." value={post.body} onChange={e => setBody(e.target.value)}></textarea>
                            <div className="images">
                            </div>
                            <CardBody>
                                {filesContent.map((file, index) => (
                                    <div key={index}>
                                        <h2>{file.name}</h2>
                                        <img alt={file.name} src={file.content}></img>
                                        <br />
                                    </div>
                                ))}
                            </CardBody>
                            <CardFooter className="text-muted">
                                <Button type="submit" variant="primary" disabled={!post.body}>
                                    Share <ChevronBarRight />
                                </Button>
                                <Button className="float-end" onClick={() => openFilePicker()}>
                                    Select files <Camera />
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