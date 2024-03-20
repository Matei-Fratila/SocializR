import { Album as AlbumModel, SelectItem, RelationType, Profile as ProfileModel } from "../types/types";
import React from "react";
import profileService from "../services/profile.service";
import { Button, Col, Container, Row } from "react-bootstrap";
import { Pencil, PersonFill, Plus } from "react-bootstrap-icons";
import { Link, useParams } from "react-router-dom";
import authService from "../services/auth.service";
import PostList from "./PostList";
import Select from 'react-select';
import Album from "./Album";
import friendshipService from "../services/friendship.service";
import axiosInstance from "../helpers/axios-helper";
import { Avatar } from "@mui/material";

const Profile = () => {
    const { id } = useParams();

    const [profile, setProfile] = React.useState<ProfileModel>({
        id: "",
        avatar: "",
        relationToCurrentUser: RelationType.Unknown,
        firstName: "",
        lastName: "",
        birthDate: new Date(),
        city: { label: "", value: "" },
        county: { label: "", value: "" },
        gender: { label: "", value: "" },
        isPrivate: false,
        nrOfFriends: 0,
        nrOfPosts: 0,
        nrOfPhotos: 0,
        description: "",
        mutualFriends: 0,
        interests: [] as SelectItem[],
        albums: [] as AlbumModel[]
    });

    const handleFetchProfile = React.useCallback(async () => {
        try {
            if (id !== undefined) {
                const result = await profileService.getProfileAsync(id);
                setProfile(result);
            }
        }
        catch {
        }
    }, [id]);

    const unfriend = async () => {
        try {
            await friendshipService.deleteFriend(profile.id);
            location.reload();
        } catch (e) {

        }
    }

    const addFriend = async () => {
        try {
            await friendshipService.addFriend(profile.id);
            location.reload();
        } catch (e) {

        }
    }

    const sendFriendRequest = async () => {
        try {
            await friendshipService.createFriendRequest(profile.id);
            location.reload();
        } catch (e) {
            console.log(e);
        }
    }

    const deleteFriendRequest = async () => {
        try {
            await friendshipService.deleteFriendRequest(profile.id);
            location.reload();
        } catch (e) {

        }
    }

    const handleDeleteAlbum = (album: AlbumModel) => {
        setProfile({ ...profile, albums: profile.albums.filter((a: AlbumModel) => a.id !== album.id) });
    }

    React.useEffect(() => {
        handleFetchProfile();
    }, [id]);

    function renderButton(relation: RelationType) {
        switch (relation) {
            case RelationType.Blocked:
                return "";
            case RelationType.Friends:
                return (<Button variant="outline-danger" size="sm" onClick={unfriend}>Unfriend</Button>);
            case RelationType.Strangers:
                return (<Button variant="outline-primary" size="sm" onClick={sendFriendRequest}>Send Friend Request</Button>);
            case RelationType.PendingAccept:
                return (<><Button variant="outline-success" size="sm" onClick={addFriend}>Accept friend request</Button><Button variant="outline-danger" size="sm">Reject friend request</Button></>);
            case RelationType.RequestedFriendship:
                return (<Button variant="outline-primary" size="sm" onClick={deleteFriendRequest}>Delete Friend Request</Button>);
            default:
                return "";
        }
    }

    return (
        <Container>
            <Row>
                <Col lg={6} md={6} sm={12} xs={12}>
                    <Row>
                        <Col lg={3} md={4} sm={3} xs={6}>
                            <Avatar alt={`${profile.firstName} ${profile.lastName}`}
                                src={`${axiosInstance.defaults.baseURL}${profile.avatar}`}
                                sx={{ width: '5em', height: '5em' }} />
                        </Col>
                        <Col lg={9} md={8} sm={9} xs={6}>
                            <h4>{profile.firstName} {profile.lastName}</h4>
                            <Link to={`/profile/friends/${id}`}><b>{profile.nrOfFriends}</b> prieteni</Link>
                            {(id !== authService.getCurrentUserId() && profile.mutualFriends !== 0)
                                && <Row><span><PersonFill /> <b>{profile.mutualFriends}</b> prieteni comuni</span></Row>}
                            <a href="#albums"><Row><span> <b>{profile.nrOfPhotos}</b> fotografii</span></Row></a>
                            <a href="#posts"><Row><span> <b>{profile.nrOfPosts}</b> postări</span></Row></a>
                        </Col>
                    </Row>
                    <div className="mt-4">
                        {(id === authService.getCurrentUserId())
                            ? <Link to={`/profile/edit/${id}`}>
                                <Pencil /> Editează profil
                            </Link> : renderButton(profile.relationToCurrentUser)}
                    </div>

                    <Row className="mt-4">
                        <h5>Informații personale</h5>
                        <hr />
                        <Col sm={4} xs={6}>
                            <dt>Nume</dt>
                        </Col>
                        <Col sm={8} xs={6}>
                            <dd>{profile.firstName}</dd>
                        </Col>
                        <Col sm={4} xs={6}>
                            <dt>Prenume</dt>
                        </Col>
                        <Col sm={8} xs={6}>
                            <dd>{profile.lastName}</dd>
                        </Col>
                        <Col sm={4} xs={6}>
                            <dt>Data nașterii</dt>
                        </Col>
                        <Col sm={8} xs={6}>
                            <dd>{new Date(profile.birthDate).toLocaleDateString()}</dd>
                        </Col>
                        <Col sm={4} xs={6}>
                            <dt>Județ</dt>
                        </Col>
                        <Col sm={8} xs={6}>
                            <dd>{profile.county.label}</dd>
                        </Col>
                        <Col sm={4} xs={6}>
                            <dt>Oraș</dt>
                        </Col>
                        <Col sm={8} xs={6}>
                            <dd>{profile.city.label}</dd>
                        </Col>
                        <Col sm={4} xs={6}>
                            <dt>Gen</dt>
                        </Col>
                        <Col sm={8} xs={6}>
                            <dd>{profile.gender.label}</dd>
                        </Col>

                        <Row>
                            <Col sm={4} xs={6}>
                                <dt>
                                    Interese
                                </dt>
                            </Col>
                            <Col sm={8} xs={12}>
                                <dd>
                                    <Select isMulti={true} value={profile.interests} isDisabled={true} />
                                </dd>
                            </Col>
                        </Row>
                    </Row>
                    <Row className="mt-4" id="albums">
                        <h5>Albume</h5>
                        <hr />
                        <div className="mb-4">
                            {(id === authService.getCurrentUserId())
                                &&
                                <Link to={`/album/create`}>
                                    <Plus /> Create a new Album
                                </Link>
                            }
                        </div>
                        {
                            profile.albums.map((album: AlbumModel) => <Album onDelete={handleDeleteAlbum} key={album.id} item={album} />)
                        }
                    </Row>
                </Col>
                <Col lg={6} md={6} sm={12} xs={12} id="#posts">
                    <h5>Postări</h5>
                    <hr />
                    {id !== undefined && <PostList key={profile.id}></PostList>}
                </Col>
            </Row>
        </Container>
    );
}

export default Profile;