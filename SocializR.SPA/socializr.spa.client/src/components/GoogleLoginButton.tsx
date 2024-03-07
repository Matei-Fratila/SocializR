import authService from '../services/auth.service';
import { useNavigate } from 'react-router-dom';
import { CredentialResponse, GoogleLogin } from '@react-oauth/google';
import React from 'react';

const GoogleLoginButton = () => {
    const [user, setUser] = React.useState<CredentialResponse>();
    const navigate = useNavigate();

    const handleFetchUser = React.useCallback(async () => {
        if (user && user.credential) {
            try {
                // const response: AxiosResponse = await axios.get(`https://www.googleapis.com/oauth2/v1/userinfo?access_token=${user.credential}`, {
                //     headers: {
                //         Authorization: `Bearer ${user.credential}`,
                //         Accept: 'application/json'
                //     }
                // });

                // authService.saveUserAndToken({
                //     id: response.data.id,
                //     firstName: response.data.given_name,
                //     lastName: response.data.family_name,
                //     profilePhoto: response.data.picture,
                //     roles: []
                // }, user.credential);

                authService.saveUserAndToken({id: "", firstName: "", lastName: "", profilePhoto: "", roles: []}, user.credential);
                await authService.googleLogin();
                navigate("/feed");
            } catch (err) {
                console.log(err)
            }
        }
    }, [user]);

    React.useEffect(() => {
        handleFetchUser();
    }, [user]);

    return (<GoogleLogin onSuccess={(response) => setUser(response)} onError={() => console.log('Login failed')}></GoogleLogin>)
}

export default GoogleLoginButton;