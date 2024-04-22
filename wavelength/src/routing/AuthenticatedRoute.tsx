import { PropsWithChildren } from "react";
import { Navigate } from "react-router-dom";
import { WavelengthPath } from "./Routes";
import { useStore } from "../stores/store";
import { useStoreValue } from "../stores/storeValue";
import { observer } from "mobx-react-lite";
import gameSessionHub from "../signalR/gameSessionHub";

/**
 *
 */
type AuthenticatedRouteProps = {
  unauthenticatedComponent?: React.ReactNode; // If the user is not authenticated, this route will be used instead.
  connectToGameSessionHub?: boolean;
};

/**
 * Wrapper component that ensures its children will only be shown if the user is configured.
 * Otherwise, the unauthenticatedComponent specified in props will be shown.
 */
const AuthenticatedRoute: React.FC<PropsWithChildren<AuthenticatedRouteProps>> =
  observer(
    ({
      children,
      unauthenticatedComponent = <Navigate to={WavelengthPath.LOGIN} />,
      connectToGameSessionHub = false,
    }) => {
      const { userStore, gameSessionStore } = useStore();
      const [user] = useStoreValue(userStore.userStoreValue);
      const [game] = useStoreValue(gameSessionStore.gameSessionStoreValue);
      const isAuthenticated = !!user;

      if (connectToGameSessionHub && isAuthenticated && game && game.id) {
        gameSessionHub.connect(game.id);
      }

      if (!connectToGameSessionHub) {
        gameSessionHub.disconnect();
      }

      if (isAuthenticated) {
        return children;
      }

      return unauthenticatedComponent;
    }
  );

export default AuthenticatedRoute;
