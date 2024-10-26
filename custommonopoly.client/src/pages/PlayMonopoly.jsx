import AuthorizationView from "../auth/AuthorizeView.jsx";
import SignOutLink from "../auth/SignOutLink.jsx";
import { useEffect } from "react";
import toast, { Toaster } from 'react-hot-toast';

function PlayMonopoly() {
    let gameDTO = {};
    function startGame() {
        //see if existing game exists
        fetch("api/game/GetExistingGame", {
            method: "GET"
        })
            .then(response => response.json())
            .then(data => {
                if (data) {
                    gameDTO = data;
                } else {
                    return fetch("api/game/StartAndGetGame", {
                        method: "POST"
                    })
                        .then(response => response.json())
                        .then(data => {
                            gameDTO = data;
                            console.log(gameDTO);
                        })
                        .catch((e) => {
                            toast.error("Unexpected error starting the game");
                        });
                }
            })
            .catch(e => {
                toast.error("Unexpected error getting the game");
            });
    }

    useEffect(startGame, []);

    return (
        <AuthorizationView>
            <Toaster/>
            <SignOutLink className="absolute top-0 right-0 m-2 " >
                Sign out
            </SignOutLink>
            <div className="flex justify-center">
                <h1 className="mt-3 border-2 inline-block rounded-xl p-4 shadow-xl">
                    Welcome to Monopoly
                </h1>
                {/*    show players*/}
            </div>

        </AuthorizationView>
    );
}

export default PlayMonopoly;