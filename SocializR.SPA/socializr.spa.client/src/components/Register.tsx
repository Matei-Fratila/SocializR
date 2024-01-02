import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";

import authService from '../services/auth.service';
import { Link } from "react-router-dom";

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
    birthDate: Yup.string()
        .required("Date of birth is required")
});

const Register = () =>
(<Formik
    initialValues={{ firstName: "", lastName: "", email: "", password: "", birthDate: "" }}
    validationSchema={LoginSchema}
    onSubmit={async (values, { setSubmitting }) => {
        console.log(values);
        await authService.register({ firstName: '', lastName: '', email: values.email, password: values.password, birthDate: new Date()});
    }}>
    {(props) => (
        <div>
            <div>
                <h2> Sign up or <Link to={`/login`}>Login</Link> if you already have an account</h2>
                <hr></hr>
                <Form>
                    <div className="row">
                        <label htmlFor="firstName" className='col-sm-3 col-form-label'>First name</label>
                        <Field
                            type="text"
                            name="firstName"
                            placeholder="Enter first name"
                            autoComplete="off"
                            className={`col-sm-7 ${props.touched.firstName && props.errors.firstName
                                ? "is-invalid"
                                : ""
                                }`}
                        />
                        <ErrorMessage
                            component="div"
                            name="firstName"
                            className="invalid-feedback"
                        />
                    </div>

                    <div className="row">
                        <label htmlFor="lastName" className='col-sm-3 col-form-label'>Last name</label>
                        <Field
                            type="text"
                            name="lastName"
                            placeholder="Enter last name"
                            autoComplete="off"
                            className={`col-sm-7 ${props.touched.lastName && props.errors.lastName
                                ? "is-invalid"
                                : ""
                                }`}
                        />
                        <ErrorMessage
                            component="div"
                            name="lastName"
                            className="invalid-feedback"
                        />
                    </div>

                    <div className="row">
                        <label htmlFor="birthDate" className='col-sm-3 col-form-label'>Date of birth</label>
                        <Field
                            type="text"
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
                    </div>

                    <div className="row">
                        <label htmlFor="email" className='col-sm-3 col-form-label'>Email</label>
                        <Field
                            type="email"
                            name="email"
                            placeholder="Enter email"
                            autoComplete="off"
                            className={`col-sm-7 ${props.touched.email && props.errors.email
                                ? "is-invalid"
                                : ""
                                }`}
                        />
                        <ErrorMessage
                            component="div"
                            name="email"
                            className="invalid-feedback"
                        />
                    </div>

                    <div className="row form-group">
                        <label htmlFor="password" className="col-sm-3 col-form-label">Password</label>
                        <Field
                            type="password"
                            name="password"
                            placeholder="Enter password"
                            className={`col-sm-7 ${props.touched.password && props.errors.password
                                ? "is-invalid"
                                : ""
                                }`}
                        />
                        <ErrorMessage
                            component="div"
                            name="password"
                            className="invalid-feedback"
                        />
                    </div>

                    <button
                        type="submit"
                        className="btn btn-primary btn-block mt-4"
                        disabled={props.isSubmitting}
                    >
                        {props.isSubmitting
                            ? "Registering..."
                            : "Register"}
                    </button>
                </Form>
            </div>
        </div>
    )}
</Formik>)

export default Register;