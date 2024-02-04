import { Outlet } from "react-router-dom";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import authService from "./services/auth.service";
import './App.css';
import { BoxArrowRight, HouseFill, PersonFill } from 'react-bootstrap-icons';
import { Link } from 'react-router-dom';
import { Button, Form, NavDropdown } from "react-bootstrap";

const App = () => {
    const user = authService.getCurrentUser();
    const isLoggedIn = user !== null;

    return (
        <>
            {isLoggedIn && 
            <header>
                <Navbar expand="lg" className="bg-body-tertiary">
                    <Container fluid>
                        <Navbar.Brand href="#">SocializR</Navbar.Brand>
                        <Navbar.Toggle aria-controls="navbarScroll" />
                        <Navbar.Collapse id="navbarScroll">
                            <Nav
                                className="me-auto my-2 my-lg-0"
                                style={{ maxHeight: '100px' }}
                                navbarScroll
                            >
                                <Nav.Link as={Link} to={`/profile/${user?.id}`}>
                                    <PersonFill /> Profile
                                </Nav.Link>
                                <Nav.Link as={Link} to={`/feed`}>
                                    <HouseFill /> Feed
                                </Nav.Link>
                                <NavDropdown title="Admin" id="navbarScrollingDropdown">
                                    <NavDropdown.Item href="#action3">
                                        Action
                                    </NavDropdown.Item>
                                    <NavDropdown.Item href="#action4">
                                        Another action
                                    </NavDropdown.Item>
                                    <NavDropdown.Divider />
                                    <NavDropdown.Item href="#action5">
                                        Something else here
                                    </NavDropdown.Item>
                                </NavDropdown>
                                <Nav.Link as={Link} to={`/login`} onClick={authService.logout}>
                                    <BoxArrowRight /> Logout
                                </Nav.Link>
                            </Nav>
                            <Form className="d-flex">
                                <Form.Control
                                    type="search"
                                    placeholder="Search"
                                    className="me-2"
                                    aria-label="Search"
                                />
                                <Button variant="outline-success">Search</Button>
                            </Form>
                        </Navbar.Collapse>
                    </Container>
                </Navbar>
            </header>}

            <main>
                <Outlet />
            </main>
        </>
    );
}

export default App;