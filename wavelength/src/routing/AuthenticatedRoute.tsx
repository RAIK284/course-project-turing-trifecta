import { PropsWithChildren } from "react";
import NotFound from "./NotFound";

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
const AuthenticatedRoute: React.FC<
  PropsWithChildren<AuthenticatedRouteProps>
> = ({ children, unauthenticatedComponent }) => {
  const isAuthenticated = false; // TODO: CHANGE THIS ONCE STORES ARE SET UP
  console.log("AuthenticatedRoute component needs to be configured"); // TODO: REMOVE THIS ONCE STORES ARE SET UP

  if (isAuthenticated) {
    return children;
  }

  if (unauthenticatedComponent) {
    return unauthenticatedComponent;
  }

  return <NotFound />;
};

export default AuthenticatedRoute;
