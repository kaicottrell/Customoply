import AuthorizationView from "../auth/AuthorizeView.jsx";
import SignOutLink from "../auth/SignOutLink.jsx";
import { useEffect, useState, useMemo } from "react";
import toast, { Toaster } from 'react-hot-toast';
import StartGameModal from '../components/StartGameModal.jsx';
import {
    playerBGColors, playerBorderColors,
    boardSquareColors, boardSquareSizes,
    getPlayerBorderColor, getPlayerHoverBGColor, boardSquareStructure, boardContentOrientation,
    rotateClasses
} from '../constants/boardConstants.js';

import CustomModal from '../components/CustomModal.jsx';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faRotate } from '@fortawesome/free-solid-svg-icons'

function PlayMonopoly() {
    const [gameDTO, setGameDTO] = useState({});
    const [isStartGameModalOpen, setStartGameModalOpen] = useState(false);
    const [activePlayer, setActivePlayer] = useState({});
    const [rotateBoardClass, setRotateBoardClass] = useState('');
    //TODO: games should be able to be gathered

    const BoardSquareDirections = {
        TOP: "TOP",
        RIGHT: "RIGHT",
        BOTTOM: "BOTTOM",
        LEFT: "LEFT",
        BOTTOM_LEFT_CORNER: "BOTTOM_LEFT_CORNER",
        BOTTOM_RIGHT_CORNER: "BOTTOM_RIGHT_CORNER",
        TOP_RIGHT_CORNER: "TOP_RIGHT_CORNER",
        TOP_LEFT_CORNER: "TOP_LEFT_CORNER"
    };
    function rotateBoard() {
        const indexOfCurrentRotation = rotateClasses.indexOf(rotateBoardClass);
        console.log("Current rotation index:", indexOfCurrentRotation);

        const newClass = rotateClasses[(indexOfCurrentRotation + 1) % rotateClasses.length];
        console.log("New rotation class:", newClass);

        setRotateBoardClass(newClass);
    }

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

        const sections = {
            BottomRightCorner: gameDTO.boardSquares[0], // first corner square
            BottomSegment: gameDTO.boardSquares.slice(0 + 1, squaresPerSide), // first segment bottom of the board
            BottomLeftCorner: gameDTO.boardSquares[squaresPerSide], /// second corner square
            LeftSegment: gameDTO.boardSquares.slice(squaresPerSide + 1, squaresPerSide * 2), //second segment: left side of the board
            TopLeftCorner: gameDTO.boardSquares[squaresPerSide * 2],
            TopSegment: gameDTO.boardSquares.slice((squaresPerSide * 2) + 1, squaresPerSide * 3),
            TopRightCorner: gameDTO.boardSquares[squaresPerSide * 3],
            RightSegment: gameDTO.boardSquares.slice((squaresPerSide * 3) + 1, squaresPerSide * 4)
        };

        console.log("boardSections:", sections);
        return sections;
    }, [gameDTO]);


    const getGameBoardSquareView = (boardSquare, index = null, boardSquareOrientation) => {
        const boardSquareSize = boardSquareSizes[boardSquare.type];
        const classBoardSquareSize = {
            "Medium":
                boardSquareOrientation == BoardSquareDirections.RIGHT || boardSquareOrientation == BoardSquareDirections.LEFT ?
                    "w-56 h-28" : "h-56 w-28",
            "Big": "size-56"
        }[boardSquareSize] || (() => { throw new Error("Size not implemented"); })();

        
        const boardSquareflexAlignment = boardSquareStructure[boardSquareOrientation];
        console.log("orientation " + boardSquareOrientation);
        console.log("boardSquareflexAlignment " + boardSquareflexAlignment);

        const boardColorDimensions = boardSquareOrientation == BoardSquareDirections.RIGHT || boardSquareOrientation == BoardSquareDirections.LEFT ?
            "w-5 h-28" : "h-5 w-28";
        const contentRotation = boardContentOrientation[boardSquareOrientation];
        const boardSquareColorBGClass = boardSquare.color ? boardSquareColors[boardSquare.color] : "";
        const boardSquareOrder = boardSquare.orderNumber;
        const playersOnSquare = gameDTO.playerList.filter((player) => player.currentPosition === boardSquareOrder);
        
        return (
            <div key={index ?? undefined} className={`flex ${classBoardSquareSize} ${boardSquareflexAlignment} border-1 border-black text-center text-xs`}>
                {
                    boardSquareColorBGClass && (
                        <div className={`${boardSquareColorBGClass} ${boardColorDimensions}`}></div>
                    )
                }
                <div className={contentRotation ?? '' }>
                    {boardSquare.name}
                </div>
                
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

    const renderBoardSection = (section, orientation) => {
        return section && section.map((boardSquare, index) => {
            return getGameBoardSquareView(boardSquare, index, orientation);
        });
    }

    const renderBoardCorner = (boardSquare, orientation) => {
        return boardSquare && getGameBoardSquareView(boardSquare, null, orientation);
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
                const errorText = await response.text();
                throw new Error(`Network response was not successful when attempting to move the player. Status: ${response.status}, StatusText: ${response.statusText}, Error: ${errorText}`);
            }

            const gameData = await response.json();
            setGameDTO(gameData);
        } catch (error) {
            console.error("Unexpected error moving the player: " + error);
            toast.error("Unexpected error moving the player: ");
        }
    }
    function getEventView() {
        const event = gameDTO.currentBoardEvent;
        if (!event) {
            return;
        }

        const propertyEventChoices = gameDTO.currentBoardEvent.propertyOptions;

        return (
            <div>
                <h3>{event.description}</h3>
                {
                    propertyEventChoices ? (
                        <>
                            <div> Price: {event.purchasePrice} </div>
                            <div className="flex justify-center gap-3 ">
                                {propertyEventChoices.map((option) => (
                                    <div key={`choice-${option}`}>
                                        <button type="button" onClick={() => handleEventResponse(option)} className="p-3 border-2 border-indigo-500 hover:border-2 hover:border-green-700 hover:bg-green-200">
                                            {option}
                                        </button>
                                    </div>
                                ))}
                            </div>
                        </>
                    ) : (
                        <button type="button" onClick={() => handleEventResponse()} className="p-3 border-2 border-indigo-500 hover:border-2 hover:border-green-700 hover:bg-green-200">
                            OK
                        </button>
                    )
                }
            </div>
        );
    }

    function handleEventResponse(option) {
        console.log("Option: " + option);

        const currentBoardEvent = gameDTO.currentBoardEvent;

        if (option) {
            const availableForPurchaseEventResponse = {
                GameId: gameDTO.id,
                BoardEvent: currentBoardEvent,
                SelectedPropertyOption: option,
            };
            console.log("available for purchase" + availableForPurchaseEventResponse);
            sendEventResponse(availableForPurchaseEventResponse);
        } else {
            const acknowledgementResponse = {
                GameId: gameDTO.id,
                BoardEvent: currentBoardEvent
            };
            console.log("acknowledge" + acknowledgementResponse);
            sendEventResponse(acknowledgementResponse);
        }
    }


    function sendEventResponse(eventResponse) {
        console.log("Sending event response:", eventResponse);
        fetch("api/game/HandleBoardEventResponse", {
            method: "POST",
            headers: {
                "content-type": "application/json"
            },
            body: JSON.stringify(eventResponse)
        })
            .then(response => {
                if (!response.ok) {
                    return response.text().then(text => { throw new Error(text) });
                }
                return response.json();
            })
            .then(data => {
                console.log("Response data:", data);
                setGameDTO(data);
                toast.success("Event handled successfully!");
            })
            .catch(error => {
                console.error("Error handling event:", error);
            });
    }




    return (
        <AuthorizationView>
            <Toaster />
            <SignOutLink className="absolute top-0 right-0 m-2 ">
                <p>Sign out </p>
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
                            <div key={player.Id} className={`border-2 ${playerColorClass} ${activePlayerAnimationClass} text-black p-3 shadow-xl rounded-xl text-center font-bold mt-5`}>
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
                        <button onClick={rollDice} className={`border-2 ${getPlayerBorderColor(activePlayer.color)} mt-3 relative p-2 rounded-md hover:text-white ${getPlayerHoverBGColor(activePlayer.color)}`} type="button">
                            <span className="animate-ping absolute inline-flex size-2 rounded-full bg-sky-400 opacity-75 right-0 top-0"></span>
                            Roll Dice
                        </button>
                    )
                }
                <button className="bg-white border-2 border-black p-3 mt-4" type="button" onClick={() => rotateBoard()}>
                    <FontAwesomeIcon icon={faRotate } />
                </button>


                <div className={`flex flex-col m-5 bg-green-200 ${rotateBoardClass}`}>
                    {/* Section Top */}
                    <div className="flex flex-row">
                        {renderBoardCorner(boardSections.TopLeftCorner, BoardSquareDirections.TOP_LEFT_CORNER)}
                        {renderBoardSection(boardSections.TopSegment, BoardSquareDirections.TOP)}
                        {renderBoardCorner(boardSections.TopRightCorner, BoardSquareDirections.TOP_RIGHT_CORNER)}
                    </div>
                    {/* Sections Left and Right*/}
                    <div className="flex flex-row justify-between">
                        <div className="flex flex-col-reverse">
                            {renderBoardSection(boardSections.LeftSegment, BoardSquareDirections.LEFT)}
                        </div>
                        <div className="flex flex-col justify-between items-center rotate-45 ">
                            <div className="outline-5 outline-dashed w-60 h-40 flex justify-center items-center">
                                <p>Community Chest Cards </p> 
                            </div>
                            <h1 className="text-9xl text-white border-2 border-black p-5 bg-red-600 shadow-xl stroke-black"> Monopoly</h1>
                            <div className="outline-5 outline-dashed w-60 h-40 flex justify-center items-center">
                                <p>Chance Cards </p> 
                            </div>
                        </div>
                        <div className="flex flex-col">
                            {renderBoardSection(boardSections.RightSegment, BoardSquareDirections.RIGHT)}
                        </div>
                    </div>
                    {/* Sections  Bottom */}
                    <div className="flex flex-row-reverse items-end">
                        {renderBoardCorner(boardSections.BottomRightCorner, BoardSquareDirections.BOTTOM_RIGHT_CORNER)}
                        {renderBoardSection(boardSections.BottomSegment, BoardSquareDirections.BOTTOM)}
                        {renderBoardCorner(boardSections.BottomLeftCorner, BoardSquareDirections.BOTTOM_LEFT_CORNER)}
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
                        {getEventView()}
                    </CustomModal>
                )
            }
        </AuthorizationView>
    );
}

export default PlayMonopoly;
