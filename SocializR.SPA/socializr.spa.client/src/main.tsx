import React from 'react';
import ReactDOM from 'react-dom/client';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App.tsx';
import Login from './components/Login.tsx';
import Register from './components/Register.tsx';
import { createBrowserRouter, Link, Navigate, RouterProvider } from 'react-router-dom';
import Feed from './components/Feed.tsx';
import Profile from './components/Profile.tsx';
import EditProfile from './components/EditProfile.tsx';
import Gallery from './components/Gallery.tsx';
import EditMedia from './components/EditMedia.tsx';
import CreateAlbum from './components/CreateAlbum.tsx';
import Friends from './components/Friends.tsx';
import Media from './components/Media.tsx';

const router = createBrowserRouter([
  {
    element: <App />,
    children: [
      {
        path: "feed",
        element: <Feed />
      },
      {
        path: "profile/:id",
        element: <Profile />,
        handle: {
          crumb: () => <Link to="/profile/:id">Profile</Link>
        }
      },
      {
        path: "profile/edit/:id",
        element: <EditProfile />,
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

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
)
