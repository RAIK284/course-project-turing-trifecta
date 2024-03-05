import { defineConfig } from "vite";
import react from "@vitejs/plugin-react-swc";

// https://vitejs.dev/config/
export default defineConfig({
  envPrefix: "APP_",
  server: {
    port: 3000,
    host: "127.0.0.1",
  },
  preview: {
    port: 3000,
  },
  plugins: [react()],
  build: {
    target: ["chrome89", "edge89", "firefox89", "safari15"],
  },
});
