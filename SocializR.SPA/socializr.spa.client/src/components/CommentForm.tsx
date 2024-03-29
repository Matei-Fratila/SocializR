import { Button, Card, CardFooter, Container } from "react-bootstrap";
import commentService from "../services/comment.service";
import { Comment as Comm, CommentFormProps } from "../types/types";
import React from "react";

const CommentForm = ({ postId, onSubmit }: CommentFormProps) => {
    const [body, setBody] = React.useState("");

    async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget;
        const formData = new FormData(form);

        try {
            const comment: Comm = await commentService.createComment(formData);
            setBody("");
            onSubmit(comment);
        } catch (e) {
            console.error(e);
        }
    }

    return (
        <Container className="mt-3">
            <form method="post" onSubmit={handleSubmit}>
                <input type="hidden" name="postId" value={postId}></input>
                <Card>
                    <textarea name="body" value={body} placeholder="Adaugă un comentariu" onChange={e => setBody(e.target.value)} />
                    <CardFooter>
                        <Button type="submit" variant="primary" disabled={!body}>
                            Comentează
                        </Button>
                    </CardFooter>
                </Card>
            </form >
        </Container>
    );
};

export default CommentForm;