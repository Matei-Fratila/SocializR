import { SelectItem } from "../types/types";
import React from "react";
import profileService from "../services/profile.service";
import { Button, Col, Row } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import "./Profile.css";
import * as Yup from 'yup';
import { ErrorMessage, Field, Form, Formik } from "formik";
import Select from "react-select";
import locationService from "../services/selectItems.service";
import selectItemsService from "../services/selectItems.service";
import { PencilFill } from "react-bootstrap-icons";

const EditProfile = () => {
    const navigate = useNavigate();
    const { id } = useParams();
    const [profile, setProfile] = React.useState({
        id: "",
        userPhoto: "",
        firstName: "",
        lastName: "",
        birthDate: new Date(),
        city: null,
        county: null,
        gender: "",
        isPrivate: false,
        description: "",
        interests: [] as SelectItem[]
    });

    const [genders, setGenders] = React.useState([] as SelectItem[]);
    const [counties, setCounties] = React.useState([] as SelectItem[]);
    const [cities, setCities] = React.useState([] as SelectItem[]);
    const [interests, setInterests] = React.useState([] as SelectItem[]);
    const [file, setFile] = React.useState(null);

    const ProfileSchema = Yup.object().shape({
        firstName: Yup.string()
            .required("First Name is required"),
        lastName: Yup.string()
            .required("Last Name is required"),
        // county: Yup.string()
        //     .required("County is required"),
        // city: Yup.string()
        //     .required("City is required"),
    });

    const handleFetchProfile = React.useCallback(async () => {
        try {
            if (id !== undefined) {
                const profile = await profileService.getProfileAsync(id);
                setProfile(profile);
            }
        }
        catch {
        }
    }, [id]);

    const handleFetchCities = async () => {
        try {
            const citiesResult = await locationService.getCities(profile.county.value);
            setCities(citiesResult);
        } catch {

        }
    }

    const handleFetchCounties = async () => {
        try {
            const countiesResult = await locationService.getCounties();
            setCounties(countiesResult);
        } catch {

        }
    }

    const handleFetchInterests = async () => {
        try {
            const interestsResult = await selectItemsService.getInterests();
            setInterests(interestsResult);
        } catch {

        }
    }

    const handleFetchGenders = async () => {
        try {
            const gendersResult = await selectItemsService.getGenders();
            setGenders(gendersResult);
        } catch {

        }
    }

    React.useEffect(() => {
        handleFetchCounties();
        handleFetchInterests();
        handleFetchGenders();
    }, []);

    React.useEffect(() => {
        if (profile.county !== null) {
            handleFetchCities();
        }
    }, [profile.county]);


    React.useEffect(() => {
        handleFetchProfile();
    }, [id]);

    return (
        <Row>
            <Col>
                <h5><PencilFill /> Edit profile</h5>
                <hr />
                <img className="rounded-circle profile-user-photo shadow img-thumbnail float-center" alt="Avatar"
                    src={file !== null ? URL.createObjectURL(file[0]) : `/api/${profile.userPhoto}`} />
                <Row className="form-group mb-3 mt-3">
                    <label className='col-4 col-form-label'>Avatar</label>
                    <Col xs={8}>
                        <input className="form-control" type="file" accept="image/*" name="media" onChange={(e) => setFile(e.target.files)} />
                    </Col>
                </Row>
                <Formik
                    initialValues={{ ...profile, isPrivate: profile.isPrivate ? "true" : "false" }}
                    enableReinitialize
                    validationSchema={ProfileSchema}
                    onSubmit={async (values) => {
                        let data = null;
                        if (file !== null) {
                            data = { ...values, file: file[0] }
                        } else {
                            data = values;
                        }
                        await profileService.editProfileAsync(data);
                        navigate(`/profile/${profile.id}`);
                    }}>
                    {(props) => (
                        <Form>
                            <Row className="form-group mb-3">
                                <label htmlFor="description" className='col-4 col-form-label'>Description</label>
                                <Col xs={8}>
                                    <Field
                                        type="text"
                                        name="description"
                                        placeholder="description"
                                        autoComplete="off"
                                        className={`form-control ${props.touched.description && props.errors.description
                                            ? "is-invalid"
                                            : ""
                                            }`}
                                    />
                                </Col>
                            </Row>

                            <Row className="form-group mb-3">
                                <label htmlFor="firstName" className='col-4 col-form-label'>First Name</label>
                                <Col xs={8}>
                                    <Field
                                        type="text"
                                        name="firstName"
                                        placeholder="first name"
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

                            <Row className="form-group mb-3">
                                <label htmlFor="lastname" className="col-4 col-form-label">Last Name</label>
                                <Col xs={8}>
                                    <Field
                                        type="text"
                                        name="lastName"
                                        placeholder="last name"
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

                            <Row className="form-group mb-3">
                                <label htmlFor="birthDate" className='col-4 col-form-label'>Date of birth</label>
                                <Col xs={8}>
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

                            <Row className="form-group mb-3">
                                <label htmlFor="gender" className='col-4 col-form-label'>Gender</label>
                                <Col xs={8}>
                                    <Select
                                        value={profile.gender}
                                        options={genders}
                                        onChange={(option) => setProfile({ ...profile, gender: option })}
                                        className={` ${props.touched.gender && props.errors.gender
                                            ? "is-invalid"
                                            : ""
                                            }`}
                                    />
                                    <ErrorMessage
                                        component="div"
                                        name="gender"
                                        className="invalid-feedback"
                                    />
                                </Col>
                            </Row>

                            <Row className="form-group mb-3">
                                <label htmlFor="county" className='col-4 col-form-label'>County</label>
                                <Col xs={8}>
                                    <Select
                                        value={profile.county}
                                        options={counties}
                                        onChange={(option) => setProfile({ ...profile, county: option, city: null })}
                                        className={` ${props.touched.county && props.errors.county
                                            ? "is-invalid"
                                            : ""
                                            }`}
                                    />
                                    <ErrorMessage
                                        component="div"
                                        name="county"
                                        className="invalid-feedback"
                                    />
                                </Col>
                            </Row>

                            <Row className="form-group mb-3">
                                <label htmlFor="city" className='col-4 col-form-label'>City</label>
                                <Col xs={8}>
                                    <Select
                                        value={profile.city}
                                        options={cities}
                                        onChange={(option) => setProfile({ ...profile, city: option })}
                                        className={`${props.touched.city && props.errors.city
                                            ? "is-invalid"
                                            : ""
                                            }`}
                                    />
                                    <ErrorMessage
                                        component="div"
                                        name="city"
                                        className="invalid-feedback"
                                    />
                                </Col>
                            </Row>

                            <Row className="form-group mb-3">
                                <label htmlFor="interests" className='col-4 col-form-label'>Interests</label>
                                <Col xs={8}>
                                    <Select value={profile.interests} isMulti={true} options={interests} onChange={(option) => setProfile({ ...profile, interests: option })} />
                                </Col>
                            </Row>

                            <Row className="form-group mb-3 ">
                                <label htmlFor="isPrivate" className='col-4'>Privacy</label>
                                <Col xs={8}>
                                    <div className="form-check">
                                        <Field type="radio" name="isPrivate" value="false" className="form-check-input" />
                                        <label className="form-check-label">
                                            Public
                                        </label>
                                    </div>
                                    <div className="form-check">
                                        <Field type="radio" name="isPrivate" value="true" className="form-check-input" />
                                        <label className="form-check-label">
                                            Private
                                        </label>
                                    </div>
                                </Col>
                            </Row>

                            <Button
                                type="submit"
                                className="btn btn-primary btn-block mt-4"
                                disabled={props.isSubmitting}
                            >
                                {props.isSubmitting
                                    ? "Saving changes..."
                                    : "Save changes"}
                            </Button>
                        </Form>
                    )}
                </Formik>
            </Col>
        </Row>
    );
}

export default EditProfile;