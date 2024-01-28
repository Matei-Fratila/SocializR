import { Interest } from "../types/types";
import React from "react";
import profileService from "../services/profile.service";
import { Button, Col, Row } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import "./Profile.css";
import * as Yup from 'yup';
import { ErrorMessage, Field, Form, Formik } from "formik";

const EditProfile = () => {
    const navigate = useNavigate();
    const { id } = useParams();
    const [profile, setProfile] = React.useState({
        id: "",
        userPhoto: "",
        firstName: "",
        lastName: "",
        birthDate: new Date(),
        city: "",
        county: "",
        gender: "",
        isPrivate: false,
        description: "",
        interests: [] as Interest[],
    });

    const LoginSchema = Yup.object().shape({
        firstName: Yup.string()
        .required("First Name is required"),
    lastName: Yup.string()
        .required("Last Name is required"),
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

    return (
        <Row>
            <Col>
                <Row>
                    <Col sm={3}>
                        <img className="rounded-circle profile-user-photo shadow img-thumbnail" alt="Avatar" src={`/api/${profile.userPhoto}`} />
                    </Col>
                    <Col sm={9}>
                        <h4>{profile.firstName} {profile.lastName}</h4>
                    </Col>
                </Row>
                <Row><span>{profile.description}God loves you but not enough to save you, so good luck taking care of yourself.</span></Row>
                <Formik
                    initialValues={profile}
                    validationSchema={LoginSchema}
                    onSubmit={async (values) => {
                        console.log(values);
                        //await authService.login({ email: values.email, password: values.password });
                        navigate(`/profile/${profile.id}`);
                    }}>
                    {(props) => (
                        <Form>
                        <Row className='mb-3'>
                            <label htmlFor="firstName" className='col-sm-3 col-form-label'>First Name</label>
                            <Field
                                type="text"
                                name="firstName"
                                placeholder="first name"
                                autoComplete="off"
                                className={`col-sm-7 ${props.touched.firstName && props.errors.firstName
                                    ? "is-invalid"
                                    : ""
                                    }`}
                            />
                            <ErrorMessage
                                component="div"
                                name="email"
                                className="invalid-feedback"
                            />
                        </Row>
    
                        <Row>
                            <label htmlFor="lastname" className="col-sm-3 col-form-label">Last Name</label>
                            <Field
                                type="text"
                                name="lastName"
                                placeholder="last name"
                                className={`col-sm-7 ${props.touched.lastName && props.errors.lastName
                                    ? "is-invalid"
                                    : ""
                                    }`}
                            />
                            <ErrorMessage
                                component="div"
                                name="password"
                                className="invalid-feedback"
                            />
                        </Row>

                        <Row className="mb-3">
                        <label htmlFor="birthDate" className='col-sm-3 col-form-label'>Date of birth</label>
                        <Field
                            type="date"
                            name="birthDate"
                            placeholder="Enter your date of birth"
                            autoComplete="off"
                            className={`col-sm-7 ${props.touched.birthDate && props.errors.birthDate
                                ? "is-invalid"
                                : ""
                                }`}
                        />
                        <ErrorMessage
                            component="div"
                            name="birthDate"
                            className="invalid-feedback"
                        />
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
                    )}
                </Formik>)
            </Col>
        </Row>
    );
}

export default EditProfile;