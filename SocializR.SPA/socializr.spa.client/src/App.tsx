import 'bootstrap/dist/css/bootstrap-grid.min.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import { Outlet } from 'react-router-dom';

function App() {
    return (
        <div>
            <Outlet/>
        </div>
    );
}

export default App;