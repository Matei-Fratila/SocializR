import { Outlet } from "react-router-dom";

const Layout = () => {
    return (<>
        <div className="container">
            <div className="row">
                <div className="col-sm-3 well">
                    <div className="well">
                        <p><a href="#">My Profile</a></p>
                        <img src="bird.jpg" className="img-circle" height="65" width="65" alt="Avatar" />
                    </div>
                    <div className="well">
                        <p><a href="#">Interests</a></p>
                        <p>
                            <span className="label label-default">News</span>
                            <span className="label label-primary">W3Schools</span>
                            <span className="label label-success">Labels</span>
                            <span className="label label-info">Football</span>
                            <span className="label label-warning">Gaming</span>
                            <span className="label label-danger">Friends</span>
                        </p>
                    </div>
                    <div className="alert alert-success fade in">
                        <a href="#" className="close" data-dismiss="alert" aria-label="close">×</a>
                        <p><strong>Ey!</strong></p>
                        People are looking at your profile. Find out who.
                    </div>
                    <p><a href="#">Link</a></p>
                    <p><a href="#">Link</a></p>
                    <p><a href="#">Link</a></p>
                </div>
                <div className="col-sm-7">
                    <Outlet></Outlet>
                </div>
                <div className="col-sm-2 well">
                    <div className="thumbnail">
                        <p>Upcoming Events:</p>
                        <img src="paris.jpg" alt="Paris" width="400" height="300" />
                        <p><strong>Paris</strong></p>
                        <p>Fri. 27 November 2015</p>
                        <button className="btn btn-primary">Info</button>
                    </div>
                    <div className="well">
                        <p>ADS</p>
                    </div>
                    <div className="well">
                        <p>ADS</p>
                    </div>
                </div>
            </div>
        </div>

        <footer className="container-fluid text-center">
            <p>Footer Text</p>
        </footer>
    </>);
};

export default Layout;