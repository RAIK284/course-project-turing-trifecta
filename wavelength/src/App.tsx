import { Outlet } from "react-router-dom";
import "./index.css";
import NavBar from "./components/navbar/NavBar";
import AppBackground from "./components/AppBackground";

const App: React.FC = () => {
  return (
    <div className="App bg-theme-blue w-screen h-screen fixed">
      <AppBackground />
      <NavBar />
      {/* Children passed into the app in Routes.tsx will be output here */}
      <Outlet />
    </div>
  );
};

export default App;
