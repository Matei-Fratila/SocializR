import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import authService from '../services/auth.service';
import { Link } from 'react-router-dom';

const LoginSchema = Yup.object().shape({
    email: Yup.string()
        .email("Invalid email address format")
        .required("Email is required"),
    password: Yup.string()
        .min(3, "Password must be 3 characters at minimum")
        .required("Password is required")
});

const Login = () => {
    return (<Formik
        initialValues={{ email: "", password: "" }}
        validationSchema={LoginSchema}
        onSubmit={async (values, { setSubmitting }) => {
            console.log(values);
            await authService.login({ email: values.email, password: values.password });
        }}>
        {(props) => (
            <div>
                <div>
                    <h2> Login or <Link to={`/register`}>Register</Link> if you don't have an account</h2>
                    <hr></hr>
                    <Form>
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

                        <div className="row">
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
                                ? "Logging in..."
                                : "Login"}
                        </button>
                    </Form>
                </div>
            </div>
        )}
    </Formik>)
}

export default Login;