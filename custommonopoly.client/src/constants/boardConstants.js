// Player colors
export const playerBGColors = {
    Blue: "bg-blue-500",
    Red: "bg-red-500",
    Green: "bg-green-500",
    Yellow: "bg-yellow-500"
};

export const playerBorderColors = {
    Blue: "border-blue-500",
    Red: "border-red-500",
    Green: "border-green-500",
    Yellow: "border-yellow-500"
};

// Board square colors
export const boardSquareColors = {
    Yellow: "bg-yellow-300",
    Blue: "bg-blue-700",
    Red: "bg-red-600",
    Green: "bg-green-700",
    Orange: "bg-orange-400",
    Brown: "bg-orange-900",
    Pink: "bg-pink-400",
    "Light Blue": "bg-blue-200",
};

// Board square sizes
export const boardSquareSizes = {
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

const playerHoverBGColors = {
    Blue: "hover:bg-blue-500",
    Red: "hover:bg-red-500",
    Green: "hover:bg-green-500",
    Yellow: "hover:bg-yellow-500"
};

export const getPlayerBorderColor = (color) => {
    return playerBorderColors[color] ?? "border-gray-500";
}
export const getPlayerHoverBGColor = (color) => {
    return playerHoverBGColors[color] ?? "hover:bg-gray-500";
}
export const boardSquareStructure = {
    TOP: "flex-col-reverse justify-start",
    RIGHT: "flex-row justify-start",
    BOTTOM: "flex-col justify-start",
    LEFT: "flex-row-reverse justify-start",
    BOTTOM_LEFT_CORNER: "flex-col justify-center",
    BOTTOM_RIGHT_CORNER: "flex-col justify-center ",
    TOP_RIGHT_CORNER: "flex-col justify-center ",
    TOP_LEFT_CORNER: "flex-col justify-center"
}
export const boardContentOrientation = {
    TOP: "rotate-180  ",
    RIGHT: "rotate-270 ml-12",
    BOTTOM: "",
    LEFT: "rotate-90 mr-9",
    BOTTOM_LEFT_CORNER: "rotate-45",
    BOTTOM_RIGHT_CORNER: "rotate-315",
    TOP_RIGHT_CORNER: "rotate-225",
    TOP_LEFT_CORNER: "rotate-135"
}

export const rotateClasses = [
    "",
    "rotate-90",
    "rotate-180",
    "rotate-270",
]