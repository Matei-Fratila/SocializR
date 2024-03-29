import ReactDOM from 'react-dom/client';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App.tsx';
import Login from './components/Login.tsx';
import Register from './components/Register.tsx';
import { createBrowserRouter, Link, Navigate, RouterProvider } from 'react-router-dom';
import Feed from './components/Feed.tsx';
import Profile from './components/Profile.tsx';
import ProfileEdit from './components/ProfileEdit.tsx';
import Gallery from './components/Gallery.tsx';
import EditMedia from './components/EditMedia.tsx';
import CreateAlbum from './components/CreateAlbum.tsx';
import Friends from './components/Friends.tsx';
import axios from 'axios';
import Media from './components/Media.tsx';
import { GoogleOAuthProvider } from '@react-oauth/google';
import Mushroom from './components/Mushroom/Mushroom.tsx';
import MushroomSearch from './components/Mushroom/MushroomSearch.tsx';
import MushroomList from './components/Mushroom/MushroomList.tsx';
import MushroomEdit from './components/Mushroom/MushroomEdit.tsx';
import Layout from './components/Layout.tsx';
import { Container } from '@mui/material';
import { MushroomsGraph } from './components/Mushroom/Graph/MushroomsGraph.tsx';
import Game from './components/Mushroom/Game.tsx';

const router = createBrowserRouter([
  {
    element: <App />,
    children: [
      {
        path: "profile/:id",
        element: <Profile />,
        handle: {
          crumb: () => <Link to="/profile/:id">Profile</Link>
        }
      },
      {
        path: "profile/edit/:id",
        element: <ProfileEdit />,
        handle: {
          crumb: () => <Link to="/profile/edit/:id">Edit profile</Link>
        }
      },
      {
        path: "album/gallery/:id",
        element: <Gallery />
      },
      {
        path: "media/:id",
        element: <Media />
      },
      {
        path: "media/edit/:id",
        element: <EditMedia />
      },
      {
        path: "album/create",
        element: <CreateAlbum />
      },
      {
        path: "profile/friends/:id",
        element: <Friends />
      },
      {
        path: "mushrooms/:id",
        element: <Container><Mushroom /></Container>
      },
      {
        path: "mushrooms/edit/:id",
        element: <Container><MushroomEdit /></Container>
      },
      {
        path: "mushrooms/search",
        element: <MushroomSearch />
      },
      {
        path: "mushrooms",
        element: <MushroomList />
      },
      {
        path: "mushrooms/graph",
        element: <MushroomsGraph></MushroomsGraph>
      },
      {
        path: "mushrooms/game",
        element: <Game></Game>
      },
      {
        path: "feed",
        element: <Feed />
      }
      // {
      //   element: <Layout />,
      //   children: [
      //     {
      //       path: "feed",
      //       element: <Feed />
      //     }]
      // },
    ],
  },
  {
    path: "login",
    element: <Login />
  },
  {
    path: "register",
    element: <Register />
  },
  {
    path: "/",
    element: <Navigate to="/feed" />
  }
]);

axios.defaults.baseURL = (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') ? "/api/" : "";

ReactDOM.createRoot(document.getElementById('root')!).render(
  <GoogleOAuthProvider clientId='175734963131-86log97gsai6r4p063umtslbc05sdfbo.apps.googleusercontent.com'>
    {/* <React.StrictMode> */}
      <RouterProvider router={router} />
    {/* </React.StrictMode> */}
  </GoogleOAuthProvider>
)
