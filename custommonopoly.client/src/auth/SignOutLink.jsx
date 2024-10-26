import toast, { Toaster } from 'react-hot-toast';
import { useNavigate } from 'react-router-dom';

function SignOutLink(props) {
    const navigate = useNavigate();
    async function singOut() {
        const response = await fetch("/signout", { method: "POST" });
        if (response.ok) {
            navigate("/signin");
        } else {
            toast.error("Sign out unsuccessful, please try again.");
        }

    }
    return (
        <>
            <Toaster />
            <button className={`p-2 bg-indigo-600 text-gray-100 border-2 border-black  hover:bg-black  ${props.className}`} onClick={singOut}>
                {props.children}
            </button>
          
            
        </>

    );

}

export default SignOutLink;