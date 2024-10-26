import React, { createContext, useState, useEffect } from 'react';
import { Navigate } from 'react-router-dom';

const UserContext = createContext("");

function AuthorizeView(props) {
    const [email, setEmail] = useState("");
    const [isAuthorized, setIsAuthorized] = useState(false);
    const [isLoading, setIsLoading] = useState(true);

    //use effect happens when the component is first mounted (loaded), when the component is actuall added to the web page
    useEffect(() => {
        let retryCount = 0;
        const maxRetries = 3;
        const delay = 200;

        async function verifyLoginWithRetry() {
            try {
                let response = await fetch("/account/pingauth", { method: "GET" });
                if (response.status === 200) {
                    const contentType = response.headers.get("content-type");

                    if (contentType && contentType.indexOf("application/json") !== -1) {
                        let pingData = await response.json();
                        setEmail(pingData.email);
                        setIsAuthorized(true);
                    } else {
                        console.log("Unexpected content-type:", contentType);
                        let responseBody = await response.text();
                        console.log("Response body:", responseBody);
                        throw new Error("Expected JSON result");
                    }
                } else if (response.status === 401) {
                    console.log("Unauthorized");
                } else {
                    throw new Error(response.status);
                }
            } catch (error) {
                retryCount++;
                if (retryCount <= maxRetries) {
                    await wait(delay);
                    return verifyLoginWithRetry();
                } else {
                    console.error("Max retries reached", error);
                }
            }
        }

        function wait(delay) {
            return new Promise((resolve) => setTimeout(resolve, delay));
        }

        verifyLoginWithRetry()
            .catch(error => {
                console.error("Unexpected error while pinging for authentication", error);
            })
            .finally(() => {
                setIsLoading(false);
            });

    }, []);

    if (isLoading) {
        return <p>Loading...</p>;
    } else if (!isAuthorized) {
        return <Navigate to="/signin" replace={true} />;
    } else {
        return (
            <UserContext.Provider value={email}>
                {props.children}
            </UserContext.Provider>
        );
    }
}

export function AuthorizedUser(props) {
    // consumes the context
    const userEmail = React.useContext(UserContext);

    if (props.value === "email") {
        return <>{userEmail}</>;
    } else {
        return <></>;
    }
}

export default AuthorizeView;
