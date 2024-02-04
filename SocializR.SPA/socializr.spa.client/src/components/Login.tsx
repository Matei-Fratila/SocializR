import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import authService from '../services/auth.service';
import { Link, useNavigate } from 'react-router-dom';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/esm/Row';
import Button from 'react-bootstrap/esm/Button';
import { Col } from 'react-bootstrap';

const LoginSchema = Yup.object().shape({
    email: Yup.string()
        .email("Invalid email address format")
        .required("Email is required"),
    password: Yup.string()
        .min(3, "Password must be 3 characters at minimum")
        .required("Password is required")
});

const Login = () => {
    const navigate = useNavigate();

    return (<Formik
        initialValues={{ email: "", password: "" }}
        validationSchema={LoginSchema}
        onSubmit={async (values) => {
            console.log(values);
            await authService.login({ email: values.email, password: values.password });
            navigate("/feed");
        }}>
        {(props) => (
            <Container>
                <h5> Login or <Link to={`/register`}>Register</Link> if you don't have an account</h5>
                <hr/>
                <Form>
                    <Row className='mb-3 form-group'>
                        <Col xs={12} sm={2} className='col-form-label'>
                            <label htmlFor="email">Email</label>
                        </Col>
                        <Col xs={12} sm={8} md={6} lg={4}>
                            <Field
                                type="email"
                                name="email"
                                placeholder="Enter email"
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
                        <Col xs={12} sm={2} className='col-form-label'>
                            <label htmlFor="password">Password</label>
                        </Col>
                        <Col xs={12} sm={8} md={6} lg={4}>
                            <Field
                                type="password"
                                name="password"
                                placeholder="Enter password"
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
                            ? "Logging in..."
                            : "Login"}
                    </Button>
                </Form>
            </Container>
        )}
    </Formik>)
}

export default Login;