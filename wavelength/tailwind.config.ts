import type { Config } from "tailwindcss";

/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,jsx,ts,tsx}"],
  safelist: ["bg-star-1", "bg-star-2", "bg-star-3"], // These are values that need to be added when we conditionally use tailwind values in components
  theme: {
    extend: {
      backgroundImage: {
        stars: "url('/src/assets/stars.png')",
      },
    },
    colors: {
      "theme-blue": "#0E1228",
      "cover-blue": "#6EB4B1",
      "scoreboard-blue": "#006081",
      "scoreboard-dark-blue": "#0C2C3D",
      "center-red": "#CE2435",
      "target-4": "#5C829C",
      "target-3": "#EF473E",
      "target-2": "#EBAA21",
      "selector-red": "#D22736",
      "team-1-score-holder": "#CD9529",
      "team-2-score-holder": "#E86733",
      white: "#FFFFFF",
      black: "#000000",
      "star-1": "#4e5ba3",
      "star-2": "#2c3670",
      "star-3": "#2f3557",
      "white-hover": "#f1f1f1",
      "grey-spinner-text": "#262626",
      "ghost-guess": "#347982",
      "is-left-guess": "#dec610",
    },
  },
  plugins: [],
} satisfies Config;
