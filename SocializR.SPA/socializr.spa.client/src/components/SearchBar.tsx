import React from "react";
import { Button, Form, ListGroup } from "react-bootstrap";
import userService from "../services/user.service";
import axiosInstance from '../helpers/axios-helper';
import { User } from "../types/types";
import { Link } from "react-router-dom";

const SearchBar = () => {
    const [searchKey, setSearchKey] = React.useState("");
    const [users, setUsers] = React.useState<User[]>([]);

    const handleChange = (event: any) => {
        setSearchKey(event.target.value)
    }

    const fetchUsers = React.useCallback(async () => {
        try {
            var usersResult = await userService.searchUsers(searchKey, 0);
            setUsers(usersResult);
        } catch (e) {
            console.error(e);
        }
    }, [searchKey])

    React.useEffect(() => {
        fetchUsers();
    }, [searchKey])

    return (
        <>
            <Form className="d-flex">
                <Form.Control
                    type="search"
                    placeholder="Search"
                    className="me-2"
                    aria-label="Search"
                    name="searchKey"
                    onChange={handleChange}
                />
                <Button variant="outline-success">Search</Button>
            </Form>
            {
                users &&
                <ListGroup>
                    {users.map((user: User) => (
                        <>
                            <Link to={`{}`}>
                                <ListGroup.Item key={user.id} action>
                                    <img className="rounded-circle user-photo shadow img-thumbnail" src={`${axiosInstance.defaults.baseURL}${user.profilePhoto}`} />
                                    {user.firstName} {user.lastName}
                                </ListGroup.Item>
                            </Link>
                        </>
                    ))}
                </ListGroup>
            }
        </>
    );
}

export default SearchBar;