import {
    createBrowserRouter,
    createRoutesFromElements,
    Route,
    RouterProvider,
} from "react-router-dom";
import PlayMonopoly from "pages/PlayMonopoly";
import Register from "pages/Register";
import SignIn from "pages/SignIn";
import AuthorizationView from "auth/AuthorizeView";
import './App.css';

function App() {
    const router = createBrowserRouter(
        <>
            <Route path="/" element={PlayMonopoly} />
            <Route path="/register" element={Register} />
            <Route path="/signin" element={SignIn} />
        </>
    );

   

    
    return (
        <RouterProvider router={ router }>
    );
    
   
}

export default App;