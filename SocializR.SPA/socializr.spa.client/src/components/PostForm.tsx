import { Button, Card, CardFooter, Col, Container, Form, Row } from "react-bootstrap";
import { Pencil } from "react-bootstrap-icons";
import { Post } from "../types/types";
import { SubmitHandler, useForm } from "react-hook-form";
import postsService from "../services/posts.service";
import { TextareaAutosize } from "@mui/material";

export type NewPost = {
    title: string;
    body: string;
    media: File[];
}

interface PostFormProps {
    onSubmitPost: (post: Post) => void;
}

const PostForm = ({ onSubmitPost }: PostFormProps) => {
    const {register, handleSubmit, watch} = useForm<NewPost>({defaultValues: { title: "", body: "", media: [] }});
    const watchInfo = watch(["media"]);

    const onSubmit: SubmitHandler<NewPost> = async (data) => {
        try{
            const post: Post = await postsService.createPost(data);
            onSubmitPost(post);
        } catch (err){
            console.log(err);
        }
    }

    return (
        <Container className="mb-5">
            <Row>
                <Col sm={2}>
                </Col>
                <Col sm={10} xs={12}>
                    <Form method="post" onSubmit={handleSubmit(onSubmit)}>
                        <h5><Pencil /> Creează o postare</h5>
                        <Card className="shadow">
                            <input className="card-header" placeholder="Titlu" {...register("title")}/>
                            <TextareaAutosize className="card-body" placeholder="Scrie o postare" {...register("body")}/>
                            {watchInfo[0] && Array.from(watchInfo[0]).map((file) => (
                            <>
                                {file.type.match('image.*') && <img alt="not found" className="card-img-bottom" src={URL.createObjectURL(file)} />}
                                {file.type.match('video.*') && <video controls className="card-img-bottom" src={URL.createObjectURL(file)} />}
                            </>
                            ))}
                            <input className="form-control" type="file" accept="image/*, video/*" {...register("media")}/>
                            <CardFooter className="text-muted">
                                <Button type="submit" variant="primary">
                                    Distribuie
                                </Button>
                            </CardFooter>
                        </Card>
                    </Form>
                </Col>
            </Row>
        </Container>
    );
}

export default PostForm;