import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";

import authService from '../services/auth.service';
import { Link, useNavigate } from 'react-router-dom';
import Row from "react-bootstrap/esm/Row";
import Container from "react-bootstrap/esm/Container";
import Button from "react-bootstrap/esm/Button";
import { RegisterRequest } from "../types/types";
import { Col } from "react-bootstrap";

const LoginSchema = Yup.object().shape({
    firstName: Yup.string()
        .required("First Name is required"),
    lastName: Yup.string()
        .required("Last Name is required"),
    email: Yup.string()
        .email("Invalid email address format")
        .required("Email is required"),
    password: Yup.string()
        .min(3, "Password must be 3 characters at minimum")
        .required("Password is required"),
    birthDate: Yup.date()
        .required("Date of birth is required")
});

const Register = () => {
    const navigate = useNavigate();

    return (<Formik
        initialValues={{ firstName: "", lastName: "", email: "", password: "", birthDate: new Date() }}
        validationSchema={LoginSchema}
        onSubmit={async (values) => {
            console.log(values);
            await authService.register(values as RegisterRequest);
            navigate("/login");
        }}>
        {(props) => (
            <Container>
                <h5> Sign up or <Link to={`/login`}>Login</Link> if you already have an account</h5>
                <hr></hr>
                <Form>
                    <Row className="mb-3">
                        <Col xs={12} sm={3} md={2} className='col-form-label'>
                            <label htmlFor="firstName">First name</label>
                        </Col>
                        <Col xs={12} sm={8} md={6} lg={4}>
                            <Field
                                type="text"
                                name="firstName"
                                placeholder="Enter your first name"
                                autoComplete="off"
                                className={`form-control ${props.touched.firstName && props.errors.firstName
                                    ? "is-invalid"
                                    : ""
                                    }`}
                            />
                            <ErrorMessage
                                component="div"
                                name="firstName"
                                className="invalid-feedback"
                            />
                        </Col>
                    </Row>

                    <Row className="mb-3">
                        <Col xs={12} sm={3} md={2} className='col-form-label'>
                            <label htmlFor="lastName">Last name</label>
                        </Col>
                        <Col xs={12} sm={8} md={6} lg={4}>
                            <Field
                                type="text"
                                name="lastName"
                                placeholder="Enter your last name"
                                autoComplete="off"
                                className={`form-control ${props.touched.lastName && props.errors.lastName
                                    ? "is-invalid"
                                    : ""
                                    }`}
                            />
                            <ErrorMessage
                                component="div"
                                name="lastName"
                                className="invalid-feedback"
                            />
                        </Col>
                    </Row>

                    <Row className="mb-3">
                        <Col xs={12} sm={3} md={2} className='col-form-label'>
                            <label htmlFor="birthDate">Date of birth</label>
                        </Col>
                        <Col xs={12} sm={8} md={6} lg={4}>
                            <Field
                                type="date"
                                name="birthDate"
                                placeholder="Enter your date of birth"
                                autoComplete="off"
                                className={`form-control ${props.touched.birthDate && props.errors.birthDate
                                    ? "is-invalid"
                                    : ""
                                    }`}
                            />
                            <ErrorMessage
                                component="div"
                                name="birthDate"
                                className="invalid-feedback"
                            />
                        </Col>
                    </Row>

                    <Row className="mb-3">
                        <Col xs={12} sm={3} md={2} className='col-form-label'>
                            <label htmlFor="email">Email</label>
                        </Col>
                        <Col xs={12} sm={8} md={6} lg={4}>
                            <Field
                                type="email"
                                name="email"
                                placeholder="Enter your email"
                                autoComplete="off"
                                className={`form-control ${props.touched.email && props.errors.email
                                    ? "is-invalid"
                                    : ""
                                    }`}
                            />
                            <ErrorMessage
                                component="div"
                                name="email"
                                className="invalid-feedback"
                            />
                        </Col>
                    </Row>

                    <Row className="form-group">
                        <Col xs={12} sm={3} md={2} className='col-form-label'>
                            <label htmlFor="password">Password</label>
                        </Col>
                        <Col xs={12} sm={8} md={6} lg={4}>
                            <Field
                                type="password"
                                name="password"
                                placeholder="Enter your password"
                                className={`form-control ${props.touched.password && props.errors.password
                                    ? "is-invalid"
                                    : ""
                                    }`}
                            />
                            <ErrorMessage
                                component="div"
                                name="password"
                                className="invalid-feedback"
                            />
                        </Col>
                    </Row>

                    <Button
                        type="submit"
                        className="btn btn-primary btn-block mt-4"
                        disabled={props.isSubmitting}
                    >
                        {props.isSubmitting
                            ? "Registering..."
                            : "Register"}
                    </Button>
                </Form>
            </Container>
        )}
    </Formik>)
}

export default Register;