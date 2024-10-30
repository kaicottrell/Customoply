import React, { useState } from 'react';
import CustomModal from './CustomModal.jsx';
function StartGameModal({ isOpen, onStartGame }) {
    const [numPlayers, setNumPlayers] = useState(1);

    function handleStartGame() {
        onStartGame(numPlayers);
    }

    if (!isOpen) {
        return null;
    }

    return (
        <CustomModal>
            <h3 className="border-b-2 border-indigo-500 pb-2 mb-3">Select the Number of Players</h3>
            <select value={numPlayers} onChange={(e) => setNumPlayers(parseInt(e.target.value))}>
                {[1, 2, 3, 4].map((num) => (
                    <option key={num} value={num}>{num}</option>
                ))}
            </select>
            <button type="button" onClick={handleStartGame} className="mt-4 bg-indigo-500 text-white px-4 py-2 rounded">
                Start Game
            </button>
        </CustomModal>
    );
}

export default StartGameModal;
