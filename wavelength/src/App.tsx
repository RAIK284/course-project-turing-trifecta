import { Outlet } from "react-router-dom";
import "./index.css";

const App: React.FC = () => {
  return (
    <div className="App bg-theme-blue w-screen h-screen">
      {/* Children passed into the app in Routes.tsx will be output here */}
      <Outlet />
    </div>
  );
};

export default App;
