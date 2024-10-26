import { useState } from 'react';
import toast, { Toaster } from 'react-hot-toast';
import { useNavigate } from "react-router-dom";


function SignIn() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [rememberMe, setRememberMe] = useState(false);

    const navigate = useNavigate();
    function attemptSignIn(e) {
        e.preventDefault();
        const targetButton = e.target.querySelector('button[type="submit"]');
        if (targetButton) targetButton.disabled = true;

        if (!email || !password) {
            toast.error('Enter both email and password.', { position: "top-right" });
            if (targetButton) targetButton.disabled = false;
        } else {
            const rememberMeString = rememberMe ? "useCookies=true" : "useSessionCookies=true";
            fetch(`/login?${rememberMeString}`, {
                method: "POST",
                headers: {
                    "content-type": "application/json"
                },
                body: JSON.stringify({
                    email: email,
                    password: password
                })
            }).then((response) => {
                if (response.ok) {
                    //route to the home screen
                    navigate("/");
                } else {
                    toast.error('Email and password combination unsuccessful.', { position: "top-right" });
                    if (targetButton) targetButton.disabled = false;
                }
                
            }).catch((e) => {
                toast.error("Unexpected error while attemping a login request." + e.error);
                if (targetButton) targetButton.disabled = false;
            });
        }
    }


    return (
        <div className="flex justify-center items-center min-h-screen">
            <Toaster />
            <form onSubmit={attemptSignIn} className="flex flex-col gap-3 p-4 max-w-md bg-white shadow-xl rounded-lg mx-auto">
                <div className="text-center">
                    <label className="block text-sm font-medium text-gray-700" htmlFor="emailInput">Enter your email:</label>
                    <input
                        id="emailInput"
                        onChange={(e) => setEmail(e.target.value)}
                        type="email"
                        className="mt-1 block w-full px-3 py-2 border
                            border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500
                            focus:border-indigo-500 sm:text-sm"
                    />
                </div>
                <div className="text-center">
                    <label htmlFor="passwordInput" className="block text-sm font-medium text-gray-700">Enter your password:</label>
                    <input
                        id="passwordInput"
                        onChange={(e) => setPassword(e.target.value)}
                        type="password"
                        className="mt-1 block w-full px-3 py-2 border border-gray-300
                            rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 
                            focus:border-indigo-500 sm:text-sm"
                    />
                </div>
                <div className="flex justify-center items-center">
                    <input
                        id="rememberMe"
                        type="checkbox"
                        checked={rememberMe}
                        onChange={(e) => setRememberMe(e.target.checked)}
                        className="h-5 w-5 text-indigo-600 border-gray-300 rounded focus:ring-indigo-500"
                    />
                    <label htmlFor="rememberMe" className="ml-2 block text-md text-gray-900">
                        Remember me
                    </label>
                </div>
                <div className="text-center">
                    <button type="submit" className="w-full py-2 px-4 bg-indigo-600 mb-2 disabled:opacity-50 
                        text-white hover:text-black font-semibold rounded-md shadow-md hover:bg-gray-700
                        focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 ">
                        Sign In
                    </button>

                    <a className="text-indigo-500 hover:text-black focus:border focus:border-indigo-500 text-md  cursor-pointer " onClick={() => navigate("/register")}>Don't have an account? Register </a>
                </div>
            </form>
        </div>
    );
}

export default SignIn;