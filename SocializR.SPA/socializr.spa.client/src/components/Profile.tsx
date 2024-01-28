import { Album as AlbumModel, Interest, RelationType } from "../types/types";
import React from "react";
import profileService from "../services/profile.service";
import { Button, Col, Row } from "react-bootstrap";
import { CameraFill, Image, InfoSquareFill, Pencil, PeopleFill, PersonFill, PostageFill } from "react-bootstrap-icons";
import { Link, useParams } from "react-router-dom";
import authService from "../services/auth.service";
import "./Profile.css";
import PostList from "./PostList";
import Select from 'react-select';
import Album from "./Album";

const Profile = () => {
    const { id } = useParams();

    const [profile, setProfile] = React.useState({
        id: "",
        userPhoto: "",
        relationToCurrentUser: RelationType.Unknown,
        firstName: "",
        lastName: "",
        birthDate: new Date(),
        city: "",
        county: "",
        gender: "",
        isPrivate: false,
        nrOfFriends: 0,
        nrOfPosts: 0,
        nrOfPhotos: 0,
        description: "",
        mutualFriends: 0,
        interests: [] as Interest[],
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

    React.useEffect(() => {
        handleFetchProfile();
    }, [id]);

    function renderButton(relation: RelationType) {
        switch (relation) {
            case RelationType.Blocked:
                return "";
            case RelationType.Friends:
                return (<Button variant="outline-danger" size="sm">Unfriend</Button>);
            case RelationType.Strangers:
                return (<Button variant="outline-primary" size="sm">Send Friend Request</Button>);
            case RelationType.PendingAccept:
                return (<><Button variant="outline-success" size="sm">Accept friend request</Button><Button variant="outline-danger" size="sm">Reject friend request</Button></>);
            case RelationType.RequestedFriendship:
                return (<Button variant="outline-primary" size="sm">Delete Friend Request</Button>);
            default:
                return "";
        }
    }

    return (
        <Row>
            <Col>
                <Row>
                    <Col sm={3}>
                        <img className="rounded-circle profile-user-photo shadow img-thumbnail" alt="Avatar" src={`/api/${profile.userPhoto}`} />
                    </Col>
                    <Col sm={9}>
                        <h4>{profile.firstName} {profile.lastName}</h4>
                        <Row><span><PeopleFill /> {profile.nrOfFriends} friends</span></Row>
                        {(id !== authService.getCurrentUserId() && profile.mutualFriends !== 0)
                            && <Row><span><PersonFill /> {profile.mutualFriends} mutual friends</span></Row>}
                        <Row><span><PostageFill /> {profile.nrOfPosts} posts</span></Row>
                        <Row><span><CameraFill /> {profile.nrOfPhotos} photos</span></Row>
                    </Col>
                </Row>
                <Row><span>{profile.description}God loves you but not enough to save you, so good luck taking care of yourself.</span></Row>
                <div className="mt-4">
                    {(id === authService.getCurrentUserId())
                        ? <Link to={`/profile/edit/${id}`}>
                            <Pencil /> Edit profile
                        </Link> : renderButton(profile.relationToCurrentUser)}
                </div>

                <Row className="mt-4">
                    <h5><InfoSquareFill /> Personal information</h5>
                    <hr />
                    <Col sm={4}>
                        <dt>First Name</dt>
                    </Col>
                    <Col sm={8}>
                        <dd>{profile.firstName}</dd>
                    </Col>
                    <Col sm={4}>
                        <dt>Last Name</dt>
                    </Col>
                    <Col sm={8}>
                        <dd>{profile.lastName}</dd>
                    </Col>
                    <Col sm={4}>
                        <dt>Birth Date</dt>
                    </Col>
                    <Col sm={8}>
                        <dd>{new Date(profile.birthDate).toLocaleDateString()}</dd>
                    </Col>
                    <Col sm={4}>
                        <dt>County</dt>
                    </Col>
                    <Col sm={8}>
                        <dd>{profile.county}</dd>
                    </Col>
                    <Col sm={4}>
                        <dt>City</dt>
                    </Col>
                    <Col sm={8}>
                        <dd>{profile.city}</dd>
                    </Col>
                    <Col sm={4}>
                        <dt>Gender</dt>
                    </Col>
                    <Col sm={8}>
                        <dd>{profile.gender}</dd>
                    </Col>

                    <Row>
                        <Col sm={2}>
                            <dt>
                                Interests
                            </dt>
                        </Col>
                        <Col sm={10}>
                            <dd>
                                <Select isMulti={true} value={profile.interests} isDisabled={true} />
                            </dd>
                        </Col>
                    </Row>
                </Row>
                <Row className="mt-4">
                    <h5><Image /> Albums</h5>
                    <hr />
                    {
                        profile.albums.map((album) => <Album key={album.id} item={album} />)
                    }
                </Row>
            </Col>
            <Col>
                {id !== undefined && <PostList userId={id}></PostList>}
            </Col>
        </Row>
    );
}

export default Profile;