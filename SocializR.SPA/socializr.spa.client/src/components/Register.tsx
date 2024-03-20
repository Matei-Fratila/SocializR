import authService from '../services/auth.service';
import { Link, useNavigate } from 'react-router-dom';
import Row from "react-bootstrap/esm/Row";
import Container from "react-bootstrap/esm/Container";
import Button from "react-bootstrap/esm/Button";
import { Card, CardBody, Col, Form } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";

export interface RegisterModel {
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    confirmPassword: string
};

const Register = () => {
    const navigate = useNavigate();
    const { register, handleSubmit, formState: { errors }, watch } = useForm<RegisterModel>();

    const onSubmit: SubmitHandler<RegisterModel> = async (data) => {
        try {
            await authService.register(data);
            navigate(`/login`);
        } catch (error) {
            console.error(error)
        }
    };

    return (
        <Container>
            <Card className="shadow p-3">
                <h5> Înscrie-te sau <Link to={`/login`}>Autentifică-te</Link> dacă ai deja un cont</h5>
                <hr></hr>
                <CardBody>
                    <Form onSubmit={handleSubmit(onSubmit)}>
                        <Row className="mb-3">
                            <Col xs={12} sm={3} md={2} className='col-form-label'>
                                <label htmlFor="firstName">Prenume</label>
                            </Col>
                            <Col xs={12} sm={8} md={6} lg={4}>
                                <Form.Control isInvalid={errors.firstName !== undefined} type="text" {...register("firstName", { required: "Câmp obligatoriu" })} />
                                <Form.Control.Feedback type="invalid" className='d-block'>
                                    {errors.firstName?.message}
                                </Form.Control.Feedback>
                            </Col>
                        </Row>

                        <Row className="mb-3">
                            <Col xs={12} sm={3} md={2} className='col-form-label'>
                                <label htmlFor="lastName">Nume</label>
                            </Col>
                            <Col xs={12} sm={8} md={6} lg={4}>
                                <Form.Control isInvalid={errors.lastName !== undefined} type="text" {...register("lastName", { required: "Câmp obligatoriu" })} />
                                <Form.Control.Feedback type="invalid" className='d-block'>
                                    {errors.lastName?.message}
                                </Form.Control.Feedback>
                            </Col>
                        </Row>

                        <Row className="mb-3">
                            <Col xs={12} sm={3} md={2} className='col-form-label'>
                                <label htmlFor="email">Email</label>
                            </Col>
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
                        </Row>

                        <Row className="mb-3">
                            <Col xs={12} sm={3} md={2} className='col-form-label'>
                                <label htmlFor="password">Parolă</label>
                            </Col>
                            <Col xs={12} sm={8} md={6} lg={4}>
                                <Form.Control isInvalid={errors.password !== undefined} type="password" {...register("password", {
                                    required: "Câmp obligatoriu",
                                    minLength: { value: 6, message: "Parola trebuie sa conțină minim 6 caractere" },
                                    pattern: {value: /^[a-zA-Z0-9!@#$%^&*]$/, message: "Parola trebuie sa conțină minim 6 caractere, o literă mare, o cifră și un caracter special"}
                                })} />
                                <Form.Control.Feedback type="invalid" className='d-block'>
                                    {errors.password?.message}
                                </Form.Control.Feedback>
                            </Col>
                        </Row>

                        <Row className="mb-3">
                            <Col xs={12} sm={3} md={2} className='col-form-label'>
                                <label htmlFor="confirmPassword">Validare parolă</label>
                            </Col>
                            <Col xs={12} sm={8} md={6} lg={4}>
                                <Form.Control isInvalid={errors.confirmPassword !== undefined} type="password"
                                    {...register("confirmPassword", {
                                        required: "Câmp obligatoriu", validate: (val: string) => {
                                            if (watch("password") !== val) return "Parolele nu se potrivesc"
                                        }
                                    })}
                                />
                                <Form.Control.Feedback type="invalid" className='d-block'>
                                    {errors.confirmPassword?.message}
                                </Form.Control.Feedback>
                            </Col>
                        </Row>

                        <Button type="submit" className="btn btn-primary btn-block mt-4">
                            Înregistrare
                        </Button>
                    </Form>
                </CardBody>
            </Card>
        </Container>)
}

export default Register;