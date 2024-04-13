import { Navigate, createBrowserRouter } from "react-router-dom";
import App from "../App";
import AuthenticatedRoute from "./AuthenticatedRoute";
import AuthenticatedLandingPage from "../pages/AuthenticatedLandingPage";
import UnauthenticatedLandingPage from "../pages/UnauthenticatedLandingPage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import GamePage from "../pages/GamePage";
import NotFound from "./NotFound";
import ProfilePage from "../pages/ProfilePage";
import RulesPage from "../pages/RulesPage";

export enum WavelengthPath {
  LANDING = "/",
  LOGIN = "/login",
  REGISTER = "/register",
  GAME = "/game/:gameSessionId",
  PROFILE = "/profile",
  RULES = "/rules",
}

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: WavelengthPath.LANDING,
        element: (
          <AuthenticatedRoute
            unauthenticatedComponent={<UnauthenticatedLandingPage />}
          >
            <AuthenticatedLandingPage />
          </AuthenticatedRoute>
        ),
      },
      {
        path: WavelengthPath.LOGIN,
        element: (
          <AuthenticatedRoute unauthenticatedComponent={<LoginPage />}>
            {/* Reverse the logic of the authenticated route component to redirect authenticated users to landing*/}
            <Navigate to={WavelengthPath.LANDING} />
          </AuthenticatedRoute>
        ),
      },
      {
        path: WavelengthPath.RULES,
        element: <RulesPage />,
      },
      {
        path: WavelengthPath.REGISTER,
        element: (
          <AuthenticatedRoute unauthenticatedComponent={<RegisterPage />}>
            {/* Reverse the logic of the authenticated route component to redirect authenticated users to landing*/}
            <Navigate to={WavelengthPath.LANDING} />
          </AuthenticatedRoute>
        ),
      },
      { path: WavelengthPath.GAME, element: <GamePage /> },
      {
        path: WavelengthPath.PROFILE,
        element: (
          <AuthenticatedRoute>
            <ProfilePage />
          </AuthenticatedRoute>
        ),
      },
      { path: "*", element: <NotFound /> },
    ],
  },
]);

export default router;
