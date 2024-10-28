import AuthorizationView from "../auth/AuthorizeView.jsx";
import SignOutLink from "../auth/SignOutLink.jsx";
import { useEffect, useState, useMemo } from "react";
import toast, { Toaster } from 'react-hot-toast';

function PlayMonopoly() {
    const [gameDTO, setGameDTO] = useState({});

    async function startGame() {
        try {
            // See if existing game exists
            const response = await fetch("api/game/GetExistingGame", {
                method: "GET",
                headers: {
                    "Accept": "application/json"
                }
            });

            if (!response.ok) {
                throw new Error("Network response was not successful getting the existing game.");
            }

            const data = await response.json();

            if (data && Object.keys(data).length > 0) {
                setGameDTO(data);
                toast.success("Existing game loaded successfully! ");
            } else {
                const newGameResponse = await fetch("api/game/StartAndGetGame", { method: "POST" });
                if (!newGameResponse.ok) {
                    throw new Error("Network response was not successful when attempting to start a new game");
                }
                const newGameData = await newGameResponse.json();
                console.log(newGameData);
                setGameDTO(newGameData);
            }
        } catch (exception) {
            toast.error("Unexpected error getting the game: " + exception);
        }
    }

    useEffect(() => {
        async function fetchData() {
            await startGame();
        }
        fetchData();
    }, []);

    const playerColors = {
        Blue: "bg-blue-500",
        Red: "bg-red-500",
        Green: "bg-green-500",
        Yellow: "bg-yellow-500"
    };

    const boardSquareColors = {
        Yellow: "bg-yellow-300",
        Blue: "bg-blue-700",
        Red: "bg-red-600",
        Green: "bg-green-700",
        Orange: "bg-orange-400",
        Brown: "bg-orange-900",
        Pink: "bg-pink-400",
        "Light Blue": "bg-blue-200",
    }

    const boardSquareSizes = {
        GoToJail: "Big",
        Jail: "Big",
        FreeParking: "Big",
        Go: "Big",
        BuildableProperty: "Medium",
        RailroadProperty: "Medium",
        Chance: "Medium",
        CommunityChest: "Medium",
        Utility: "Medium",
        Tax: "Medium"
    };


    const boardSections = useMemo(() => {
        if (!gameDTO.boardSquares) return {};

        const squaresPerSide = gameDTO.boardSquares.length / 4;
        console.log("Squares per side:", squaresPerSide); // Log the value of squaresPerSide

        return {
            Section1: gameDTO.boardSquares.slice(0, squaresPerSide + 1), // 0 - 11 (11 squares)
            Section2: gameDTO.boardSquares.slice(squaresPerSide + 1, (squaresPerSide * 2)), // 11-20 (9 squares)
            Section3: gameDTO.boardSquares.slice((squaresPerSide * 2), (squaresPerSide * 3) + 1), //20 - 31 (11 squares)
            Section4: gameDTO.boardSquares.slice(((squaresPerSide * 3) + 1), (squaresPerSide * 4)) //31 - 40 (9 squares)
        };
    }, [gameDTO.boardSquares]);

    const getGameBoardSquareView = (boardSquare, index) => {
        const boardSquareSize = boardSquareSizes[boardSquare.type];
        const classBoardSquareSize = {
            "Medium": "size-28",
            "Big": "size-56"
        }[boardSquareSize] || (() => { throw new Error("Size not implemented"); })();
        const boardSquareColorBGClass = boardSquare.color ? boardSquareColors[boardSquare.color] : "";
        const boardSquareOrder = boardSquare.orderNumber;
        const playersOnSquare = gameDTO.playerList.filter((player) => player.currentPosition === boardSquareOrder);
        return (
            <div key={index} className={`${classBoardSquareSize} border-1 border-black text-center text-xs`}>
                {
                    boardSquareColorBGClass && (
                        <div className={`${boardSquareColorBGClass} h-5 w-28`}></div>
                    )
                }
                {boardSquare.name}
                <div className="flex justify-center items-center flex-wrap mt-1">
                    {
                        // Player(s) on square
                        playersOnSquare.map((player, index) => {
                            const playerPuckColor = playerColors[player.color] || "bg-gray-500";
                            return (
                                <div key={index} className={`rounded-full size-8 ${playerPuckColor} border-1 border-black`}></div>
                            );
                        })
                    }
                </div>
            </div>
        );
    }
    const renderBoardSection = (section) => {
        return section && section.map((boardSquare, index) => {
            return getGameBoardSquareView(boardSquare, index);
        });
    }
    return (
        <AuthorizationView>
            <Toaster />
            <SignOutLink className="absolute top-0 right-0 m-2 " >
                Sign out
            </SignOutLink>
            <div className="flex flex-col justify-center items-center">
                <h1 className="mt-3 border-2 rounded-xl p-4 shadow-xl xl:w-1/3 sm:w-3/4 w-full text-center">
                    Welcome to Monopoly
                </h1>
                {/*<pre className="mt-3 border-2 inline-block rounded-xl p-4 shadow-xl bg-gray-100 text-black">*/}
                {/*    {JSON.stringify(gameDTO, null, 2)}*/}
                {/*</pre>*/}
                {gameDTO.playerList && gameDTO.playerList.map((player, index) => {
                    const playerColorClass = playerColors[player.color] || "bg-gray-200";
                    const playerNum = index + 1;
                    return (
                        <h3 key={player.Id} className={`${playerColorClass} text-black p-3 shadow-xl rounded-xl`}>
                            Player {playerNum}
                        </h3>
                    );
                })}

                { /*Show Gameboard */}
                <div className="flex flex-col m-5">
                    {/* Section 3 */}
                    <div className="flex flex-row">
                        {renderBoardSection(boardSections.Section3)}
                    </div>
                    {/* Sections  2 and 4 */}
                    <div className="flex flex-row justify-between">
                        <div className="flex flex-col-reverse">
                            {renderBoardSection(boardSections.Section2)}
                        </div>
                        <div className="flex flex-col">
                            {renderBoardSection(boardSections.Section4)}
                        </div>
                    </div>
                    {/* Sections  1 */}
                    <div className="flex flex-row-reverse items-end">
                        {renderBoardSection(boardSections.Section1)}
                    </div>
                </div>
            </div>

        </AuthorizationView>
    );
}

export default PlayMonopoly;
