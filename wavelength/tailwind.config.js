/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {},
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
    },
  },
  plugins: [],
};
