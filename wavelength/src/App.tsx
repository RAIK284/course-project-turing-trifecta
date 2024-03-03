import { Outlet } from "react-router-dom";
import "./index.css";
import NavBar from "./components/navbar/NavBar";

const App: React.FC = () => {
  return (
    <div className="App bg-theme-blue w-screen h-screen text-white">
      <NavBar />
      {/* Children passed into the app in Routes.tsx will be output here */}
      <Outlet />
    </div>
  );
};

export default App;
