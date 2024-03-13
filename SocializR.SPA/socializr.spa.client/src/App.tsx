import { Outlet } from "react-router-dom";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import authService from "./services/auth.service";
import './App.css';
import { Link } from 'react-router-dom';
import { NavDropdown } from "react-bootstrap";
import SearchBar from "./components/SearchBar";
import Muscarici from "./components/Mushroom/svgs/Muscarici";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";

const darkTheme = createTheme({
    palette: {
        mode: 'light',
    },
});

const App = () => {
    const user = authService.getCurrentUser();
    const isLoggedIn = user !== null;

    return (
        <>
            {isLoggedIn &&
                <header>
                    <Navbar expand="lg" className="bg-body-tertiary shadow">
                        <Container fluid>
                            <Navbar.Brand href="#"><Muscarici size={2}/> Muscărici</Navbar.Brand>
                            <Navbar.Toggle aria-controls="navbarScroll" />
                            <Navbar.Collapse id="navbarScroll">
                                <Nav
                                    className="me-auto my-2 my-lg-0"
                                    style={{ maxHeight: '100px' }}
                                    navbarScroll
                                >
                                    <Nav.Link as={Link} to={`/profile/${user?.id}`}>
                                        Profil
                                    </Nav.Link>
                                    <Nav.Link as={Link} to={`/feed`}>
                                        Noutăți
                                    </Nav.Link>
                                    <Nav.Link as={Link} to={`/mushrooms`}>
                                        Ghid
                                    </Nav.Link>
                                    <Nav.Link as={Link} to={`/mushrooms/graph`}>
                                        Graf
                                    </Nav.Link>
                                    <Nav.Link as={Link} to={`/mushrooms/game`}>
                                        Game
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
                                        Logout
                                    </Nav.Link>
                                </Nav>
                                <SearchBar></SearchBar>
                            </Navbar.Collapse>
                        </Container>
                    </Navbar>
                </header>}
            <ThemeProvider theme={darkTheme}>
                <CssBaseline />
                <main>
                    <Outlet />
                </main>
            </ThemeProvider>
        </>
    );
}

export default App;