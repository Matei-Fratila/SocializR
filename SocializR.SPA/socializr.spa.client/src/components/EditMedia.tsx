import { Button, Card, CardFooter, CardGroup } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import React from "react";
import albumService from "../services/album.service";
import { Media, MediaType } from "../types/types";

const EditMedia = () => {
    const navigate = useNavigate();
    const { id } = useParams();
    const [media, setMedia] = React.useState({ id: "", albumId: "", caption: "", type: MediaType.Unspecified, createdDate: "", fileName: "" });

    const handleFetchMedia = React.useCallback(async () => {
        try {
            if (id !== undefined) {
                const mediaResponse = await albumService.getMedia(id);
                setMedia(mediaResponse);
            }
        }
        catch {
        }
    }, [id]);

    React.useEffect(() => {
        handleFetchMedia();
    }, [id]);

    const handleSubmit = (event) => {
        event.preventDefault();

        const form = event.currentTarget;
        const formData = new FormData(form);
        try {
            var mediaResponse: Media = albumService.updateMedia(formData);
        } catch (e) {

        }
        navigate(`/album/gallery/${media.albumId}`);
        location.reload();
    }

    return (
        <form onSubmit={handleSubmit}>
            <CardGroup>
                <Card>
                    {media.type === MediaType.Image && <img src={`/api/${media.fileName}`} className="card-img-top" alt="..."></img>}
                    {media.type === MediaType.Video && <video controls src={`/api/${media.fileName}`} className="card-img-top"></video>}
                    <input name="id" hidden value={media.id}></input>
                    <input name="albumId" hidden value={media.albumId}></input>
                    <textarea className="card-body" name="caption" placeholder="Caption..." value={media.caption} 
                        onChange={e => setMedia({...media, caption: e.target.value})}/>
                    <CardFooter>
                        <Button type="submit" value="Submit">Save changes</Button>
                        <small className="text-muted float-end">{media.createdDate}</small>
                    </CardFooter>
                </Card>
            </CardGroup>
        </form>
    );
};

export default EditMedia;