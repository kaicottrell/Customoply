import AuthorizationView from "../auth/AuthorizeView.jsx";
import SignOutLink from "../auth/SignOutLink.jsx";
import { useEffect, useState, useMemo } from "react";
import toast, { Toaster } from 'react-hot-toast';
import StartGameModal from '../components/StartGameModal.jsx';
import { playerBGColors, playerBorderColors, boardSquareColors, boardSquareSizes, getPlayerBorderColor, getPlayerHoverBGColor } from '../constants/boardConstants.js';
import CustomModal from '../components/CustomModal.jsx';
function PlayMonopoly() {
    const [gameDTO, setGameDTO] = useState({});
    const [isStartGameModalOpen, setStartGameModalOpen] = useState(false);
    const [activePlayer, setActivePlayer] = useState({});
    //TODO: games should be able to be gathered 
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

            if (response.status === 204) {
                console.log("No response, new game");
                setStartGameModalOpen(true);
            } else {
                const data = await response.json();
                if (!data || Object.keys(data).length <= 0) {
                    throw new Error("Game found, but data was empty");
                }
                setGameDTO(data);
                toast.success("Existing game loaded successfully! ");
            }

        } catch (exception) {
            toast.error("Unexpected error getting the game: " + exception);
        }
    }

    async function createNewGame(numPlayers) {
        const newGameResponse = await fetch("api/game/StartAndGetGame", {
            method: "POST",
            headers: {
                "content-type": "application/json"
            },
            body: JSON.stringify({ numberOfPlayers: numPlayers })
        });
        if (!newGameResponse.ok) {
            throw new Error("Network response was not successful when attempting to start a new game");
        }
        const newGameData = await newGameResponse.json();
        // close the modal
        setStartGameModalOpen(false);
        setGameDTO(newGameData);
    }
    function updateActivePlayer() {
        if (!gameDTO.playerList) return;
        var activePlayer = gameDTO.playerList.find(player => player.isPlayersTurn);
        if (!activePlayer) {
            toast.error("Active player not found");
            return;
        }
        setActivePlayer(activePlayer);
    }
    useEffect(() => {
        updateActivePlayer()
    }, [gameDTO]);


    useEffect(() => {
        async function fetchData() {
            await startGame();
        }
        fetchData();
    }, []);


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
    }, [gameDTO]);

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
                <div className="flex justify-center items-center flex-wrap mt-1 gap-1">
                    {
                        // Player(s) on square
                        playersOnSquare.map((player, index) => {
                            const playerPuckColor = playerBGColors[player.color] ?? "bg-gray-500";
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
    //async?
    const rollDice = async () => {
        try {
            const response = await fetch(`api/game/MovePlayer?gameId=${gameDTO.id}`, {
                method: "POST",
                headers: {
                    "Accept": "application/json"
                }
            });

            if (!response.ok) {
                throw new Error("Network response was not successful when attempting to move the player.");
            }

            const gameData = await response.json();

            setGameDTO(gameData);
        } catch (error) {
            toast.error("Unexpected error moving the player: " + error);
        }
    }
   

    return (
        <AuthorizationView>
            <Toaster />
            <SignOutLink className="absolute top-0 right-0 m-2 ">
                Sign out
            </SignOutLink>

            
            <div className="flex flex-col justify-center items-center">
                <h1 className="mt-3 border-2 rounded-xl p-4 shadow-xl xl:w-1/3 sm:w-3/4 w-full text-center">
                    Welcome to Monopoly
                </h1>
                <div className="flex gap-2">
                    {gameDTO.playerList && gameDTO.playerList.map((player, index) => {
                        const playerColorClass = playerBorderColors[player.color] ?? "border-gray-200";
                        const activePlayerAnimationClass = player.isPlayersTurn ? 'animate-bounce' : '';
                        const playerNum = index + 1;
                        const playerCash = player.balance;
                        return (
                            <div key={player.Id} className={`border-2 ${playerColorClass} ${activePlayerAnimationClass}  text-black p-3 shadow-xl rounded-xl text-center font-bold mt-5`}>
                                <div className="text-md">
                                    Player: {playerNum}
                                </div>
                                <div className="text-sm">
                                    ${playerCash}
                                </div>
                            </div>
                        );
                    })}
                </div>
                {
                    activePlayer && (
                        <button onClick={ rollDice } className={`border-2 ${getPlayerBorderColor(activePlayer.color)} mt-3 relative p-2 rounded-md hover:text-white ${getPlayerHoverBGColor(activePlayer.color)}`} type="button">
                            <span className="animate-ping absolute inline-flex size-2 rounded-full bg-sky-400 opacity-75 right-0 top-0"></span>
                            Roll Dice
                        </button>
                    )
                }
              
               
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

            <StartGameModal isOpen={isStartGameModalOpen} onStartGame={(numPlayers) => createNewGame(numPlayers)} />

            {
                //Create handle board event functions to handle the different types of events such as: 
                // - No Action, Chance, CommunityCard (Ok Button)
                // - Choosing options from PropertyEvent 
                gameDTO.currentBoardEvent && (
                    <CustomModal>
                        <h3>{gameDTO.currentBoardEvent.description}</h3>

                    </CustomModal>
                )
            }
        </AuthorizationView>
    );
}

export default PlayMonopoly;
