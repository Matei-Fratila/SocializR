import { ListGroup, Pagination } from "react-bootstrap";
import { People } from "react-bootstrap-icons";
import { useNavigate, useParams } from "react-router-dom";
import React from "react";
import friendshipService from "../services/friendship.service";
import { User } from "../types/types";
import axios from "axios";

const Friends = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [friends, setFriends] = React.useState<User[]>([]);

    const handleFetchFriends = React.useCallback(async () => {
        try {
            if (id !== undefined) {
                const friendsResponse = await friendshipService.getAllFriends(id);
                setFriends(friendsResponse);
            }
        }
        catch {
        }
    }, [id]);

    React.useEffect(() => {
        handleFetchFriends();
    }, [id]);

    const handleClickFriend = (id: string) => {
        navigate(`/Profile/${id}`);
    }

    return (
        <>
            <h5><People /> Friends</h5>
            <hr />
            <Pagination>
                <ListGroup>
                    {friends.map((friend: User) => (
                        <>
                            <ListGroup.Item key={friend.id} action onClick={() => handleClickFriend(friend.id)}>
                                <img className="rounded-circle user-photo shadow img-thumbnail" src={`${axios.defaults.baseURL}${friend.profilePhoto}`} />
                                {friend.firstName} {friend.lastName}
                            </ListGroup.Item>
                        </>
                    ))}
                </ListGroup>
            </Pagination>
        </>
    );
};

export default Friends;