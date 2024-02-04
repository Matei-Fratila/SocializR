import { Button, Card, CardFooter, CardGroup, FormCheck } from "react-bootstrap";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import React from "react";
import albumService from "../services/album.service";
import { Media, MediaType } from "../types/types";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";

const EditMedia = () => {
    const navigate = useNavigate();
    const { id } = useParams();
    const { isCoverPhoto } = useLocation().state;
    const [media, setMedia] = React.useState({ id: "", albumId: "", caption: "", type: MediaType.Unspecified, createdDate: "", fileName: "", isCoverPhoto: false });

    const handleFetchMedia = React.useCallback(async () => {
        try {
            if (id !== undefined) {
                const mediaResponse: Media = await albumService.getMedia(id);

                setMedia(prevMedia => {
                    const updatedMedia = { ...mediaResponse, isCoverPhoto: prevMedia.isCoverPhoto };
                    console.log(updatedMedia);
                    return updatedMedia;
                  });
            }
        }
        catch {
        }
    }, [id]);

    React.useEffect(() => {
        handleFetchMedia();
    }, [id]);

    React.useEffect(() => {
        console.log("is cover photo changed");
        setMedia(prevMedia => {
            const updatedMedia = { ...prevMedia, isCoverPhoto: isCoverPhoto };
            console.log(updatedMedia);
            return updatedMedia;
          });
    }, [isCoverPhoto]);

    const handleSubmit = (event: any) => {
        event.preventDefault();

        const form = event.currentTarget;
        const formData = new FormData(form);
        try {
            albumService.updateMedia(formData);
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
                    <input name="id" readOnly hidden value={media.id}></input>
                    <input name="albumId" readOnly hidden value={media.albumId}></input>
                    <textarea className="card-body" name="caption" placeholder="Caption..." value={media.caption}
                        onChange={e => setMedia({ ...media, caption: e.target.value })} />
                    <FormCheck>
                        <FormCheckInput
                            className="form-check-input"
                            type="checkbox" 
                            name="isCoverPhoto"
                            checked={media.isCoverPhoto}
                            onChange={() => {
                                const updatedMedia = { ...media, isCoverPhoto: !media.isCoverPhoto };
                                console.log(updatedMedia.isCoverPhoto); // Check the value before updating state
                                setMedia(updatedMedia);
                              }}
                            />
                        <FormCheckLabel className="form-check-label">
                            Album cover photo
                        </FormCheckLabel>
                    </FormCheck>
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