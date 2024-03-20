import authService from '../services/auth.service';
import { Link, useNavigate } from 'react-router-dom';
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/esm/Button';
import { Card, CardBody, Col, Form, Row } from 'react-bootstrap';
import GoogleLoginButton from './GoogleLoginButton';
import { SubmitHandler, useForm } from 'react-hook-form';
import axios, { AxiosError } from 'axios';

export interface LoginModel {
    email: string,
    password: string
};

const Login = () => {
    const navigate = useNavigate();
    const { register, handleSubmit, formState: { errors }, setError } = useForm<LoginModel>();

    const onSubmit: SubmitHandler<LoginModel> = async (data) => {
        try {
            await authService.login(data);
            navigate(`/feed`);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                const statusCode = (error as AxiosError).response?.status;
                if (statusCode === 401) {
                    setError("password", { type: "custom", message: "Email sau parolă invalide" });
                    setError("email", { type: "custom", message: "Email sau parolă invalide" });
                }
            }
        };
    }

    return (
        <>
            <Container>
                <Card className='shadow p-3'>
                    <h5>Autentificare</h5>
                    <hr></hr>
                    <CardBody>
                        <Form onSubmit={handleSubmit(onSubmit)}>
                            <Form.Group as={Row} className="mb-3" controlId="email">
                                <Form.Label as={Col} xs={12} sm={3} md={2}>Email</Form.Label>
                                <Col xs={12} sm={8} md={6} lg={4}>
                                    <Form.Control isInvalid={errors.email !== undefined} type="email" {...register("email", {
                                        required: "Câmp obligatoriu", pattern: {
                                            value: /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/,
                                            message: "Adresă de email invalidă"
                                        }
                                    })} />
                                    <Form.Control.Feedback type="invalid" className='d-block'>
                                        {errors.email?.message}
                                    </Form.Control.Feedback>
                                </Col>
                            </Form.Group>

                            <Form.Group as={Row} className="mb-3" controlId="email">
                                <Form.Label as={Col} xs={12} sm={3} md={2}>Parolă</Form.Label>
                                <Col xs={12} sm={8} md={6} lg={4}>
                                    <Form.Control isInvalid={errors.password !== undefined} type="password" {...register("password", { required: "Câmp obligatoriu" })} />
                                    <Form.Control.Feedback type="invalid" className='d-block'>
                                        {errors.password?.message}
                                    </Form.Control.Feedback>
                                </Col>
                            </Form.Group>

                            <Button type="submit" className="btn btn-primary mb-3">
                                Logare
                            </Button>
                            <p>Sau logheaza-te prin <GoogleLoginButton /></p>

                        </Form>
                        Nu ai un cont? <Link to={'/register'}>Înregistreaza-te</Link>
                    </CardBody>
                </Card>
            </Container>
        </>
    );
};

export default Login;