// tailwind.config.js
module.exports = {
    purge: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
    content: [
        './src/**/*.{js,jsx,ts,tsx}',  // Adjust this to match your project structure
    ],
    theme: {
        extend: {
            rotate: {
                '135': '135deg',
                '225': '225deg',
                '270': '270deg',
                '315': '315deg'
            }
        },
    },
    variants: {
        extend: {
            backgroundColor: ['checked'],
            borderColor: ['checked'],
            opacity: ['disabled'],

        },
    },
    plugins: [
        require("@tailwindcss/forms"),
        require("@tailwindcss/typography")
    ],
}