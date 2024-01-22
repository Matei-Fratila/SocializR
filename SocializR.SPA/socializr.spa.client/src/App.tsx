import * as React from 'react';
import { Outlet } from "react-router-dom";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import authService from "./services/auth.service";
import './App.css';

const App = () => {
    const [isLoggedIn, setIsLoggedInState] = React.useState(authService.getCurrentUser() !== null);

    return (
        <>
            <header>
                <Navbar expand="lg" className="bg-body-tertiary fixed-top">
                    <Container>
                        <Navbar.Brand href="#home">SocializR</Navbar.Brand>
                        <Navbar.Toggle aria-controls="basic-navbar-nav" />
                        <Navbar.Collapse id="basic-navbar-nav">
                            <Nav className="me-auto">
                                <Nav.Link href="/login" onClick={authService.logout}>Logout</Nav.Link>
                            </Nav>
                        </Navbar.Collapse>
                    </Container>
                </Navbar>
            </header>

            <main>
                <Outlet />
            </main>

            <footer className="footer fixed-bottom mt-auto py-1 bg-dark-subtle opacity-50">
                <span className="text-muted">&copy; {new Date().getFullYear()} - SocializR</span>
            </footer>
        </>
    );
}

export default App;