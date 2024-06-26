import { Outlet, useNavigate } from "react-router-dom";
import "./index.css";
import NavBar from "./components/navbar/NavBar";
import { useStore } from "./stores/store";
import { useEffect } from "react";
import { observer } from "mobx-react-lite";
import AppLoader from "./components/AppLoader";
import { useStoreValue } from "./stores/storeValue";
import { WavelengthPath } from "./routing/Routes";

const App: React.FC = observer(() => {
  const navigate = useNavigate();
  const { userStore, gameSessionStore } = useStore();
  const { currentUser, loadingOffStart } = userStore;
  const [activeGameSession] = useStoreValue(
    gameSessionStore.gameSessionStoreValue
  );

  useEffect(() => {
    currentUser();
  }, []);

  useEffect(() => {
    if (activeGameSession) {
      navigate(
        WavelengthPath.GAME.replace(":gameSessionId", activeGameSession.id)
      );
    }
  }, [activeGameSession]);

  return (
    <div className="App pb-10 bg-stars bg-center bg-no-repeat bg-cover min-h-screen w-screen text-white">
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
