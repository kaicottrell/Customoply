import {
    createBrowserRouter,
    createRoutesFromElements,
    Route,
    RouterProvider,
} from "react-router-dom";
import PlayMonopoly from "./pages/PlayMonopoly.jsx";
import Register from "./pages/Register.jsx";
import SignIn from "./pages/SignIn.jsx";
import './App.css';

function App() {
    const router = createBrowserRouter(
        createRoutesFromElements(
            <>
                <Route path="/" element={<PlayMonopoly />} />
                <Route path="register" element={<Register />} />
                <Route path="signin" element={<SignIn />} />
            </>
        )
    );




    return (
        <>
            <RouterProvider router={router} />
        </>
    );


}

export default App;