import { Outlet } from "react-router-dom";
import "./index.css";
import NavBar from "./components/navbar/NavBar";
import AppBackground from "./components/AppBackground";
import { useStore } from "./stores/store";
import { useEffect } from "react";
import { observer } from "mobx-react-lite";
import AppLoader from "./components/AppLoader";

const App: React.FC = observer(() => {
  const { userStore } = useStore();
  const { currentUser, loadingOffStart } = userStore;

  useEffect(() => {
    currentUser();
  }, []);

  return (
    <div className="App bg-theme-blue w-screen h-screen fixed text-white">
      <AppBackground />
      {loadingOffStart ? (
        <AppLoader />
      ) : (
        <>
          <NavBar />
          {/* Children passed into the app in Routes.tsx will be output here */}
          <Outlet />
        </>
      )}
    </div>
  );
});

export default App;
