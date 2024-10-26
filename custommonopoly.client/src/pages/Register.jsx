import { useState } from 'react';
import toast, { Toaster } from 'react-hot-toast';
import { useNavigate } from 'react-router-dom';

function Register() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [disabledInput, setDisabledInput] = useState(false);

    const navigate = useNavigate();
    async function attemptRegister(e) {
        e.preventDefault()
        if (!email) {
            toast.error("Email is required");
        } else if (!password) {
            toast.error("Password is required");
        } else if (!confirmPassword) {
            toast.error("Confirm Password is required");
        } else if (password !== confirmPassword) {
            toast.error("Passwords do not match");
        } else {
            setDisabledInput(true);
            try {
                const response = await fetch("/account/register", {
                    method: "POST",
                    headers: {
                        "content-type": "application/json"
                    },
                    body: JSON.stringify({
                        Email: email,
                        Password: password
                    })
                });

                if (response.ok) {
                    navigate("/");
                } else {
                    const errorData = await response.json();
                    if (errorData.errors) {
                        errorData.errors.forEach(error => toast.error(error));
                    } else {
                        toast.error("Account may already be taken. Try again?");
                    }
                    setDisabledInput(false);
                }
            } catch (e) {
                toast.error("Unexpected error while attempting to register: " + e.message);
                setDisabledInput(false);
            }
        }
    }

    return (
        <div className="flex justify-center items-center min-h-screen">
            <Toaster />
            <form onSubmit={attemptRegister} className="flex flex-col gap-3 p-4 max-w-md bg-white shadow-xl rounded-lg mx-auto">
                <div className="text-center">
                    <label className="block text-sm font-medium text-gray-700" htmlFor="emailInput">Enter your email:</label>
                    <input
                        id="emailInput"
                        disabled={disabledInput}
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
                        disabled={disabledInput}
                        onChange={(e) => setPassword(e.target.value)}
                        type="password"
                        className="mt-1 block w-full px-3 py-2 border border-gray-300
                            rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 
                            focus:border-indigo-500 sm:text-sm"
                    />
                </div>
                <div className="text-center">
                    <label htmlFor="confirmPasswordInput" className="block text-sm font-medium text-gray-700">Confirm password:</label>
                    <input
                        id="confirmPasswordInput"
                        disabled={disabledInput}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        type="password"
                        className="mt-1 block w-full px-3 py-2 border border-gray-300
                            rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 
                            focus:border-indigo-500 sm:text-sm"
                    />
                </div>
                <div className="text-center">
                    <button type="submit" disabled={disabledInput} className="w-full py-2 px-4 bg-indigo-600 mb-2 disabled:opacity-50
                        text-white font-semibold rounded-md shadow-md hover:bg-gray-700
                        hover:text-gray-500 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2">
                        Register
                    </button>
                    <a className="text-indigo-500 focus:border focus:border-indigo-500 text-md cursor-pointer" onClick={() => navigate("/signin")}>
                        Already have an account? Sign In
                    </a>
                </div>
            </form>
        </div>
    );
}

export default Register;