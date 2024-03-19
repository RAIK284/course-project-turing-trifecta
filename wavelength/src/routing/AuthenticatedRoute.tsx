import { PropsWithChildren } from "react";
import { Navigate } from "react-router-dom";
import { WavelengthPath } from "./Routes";
import { useStore } from "../stores/store";
import { useStoreValue } from "../stores/storeValue";
import { observer } from "mobx-react-lite";

/**
 *
 */
type AuthenticatedRouteProps = {
  unauthenticatedComponent?: React.ReactNode; // If the user is not authenticated, this route will be used instead.
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
    }) => {
      const { userStore } = useStore();
      const [user] = useStoreValue(userStore.userStoreValue);
      const isAuthenticated = !!user;

      if (isAuthenticated) {
        return children;
      }

      return unauthenticatedComponent;
    }
  );

export default AuthenticatedRoute;
