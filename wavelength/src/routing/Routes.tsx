import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import AuthenticatedRoute from "./AuthenticatedRoute";
import AuthenticatedLandingPage from "../pages/AuthenticatedLandingPage";
import UnauthenticatedLandingPage from "../pages/UnauthenticatedLandingPage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import ChooseTeamPage from "../pages/ChooseTeamPage";
import NotFound from "./NotFound";
import ProfilePage from "../pages/ProfilePage";
import RulesPage from "../pages/RulesPage";

export enum WavelengthPath {
  LANDING = "/",
  LOGIN = "/login",
  REGISTER = "/register",
  CHOOSE_TEAM = "/chooseTeam/:gameSessionID", // Choosing a team occurs in a game session, so we need to access the game's ID
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
        element: <LoginPage />,
      },
      {
        path: WavelengthPath.RULES,
        element: <RulesPage />
      },
      {
        path: WavelengthPath.REGISTER,
        element: <RegisterPage />,
      },
      { path: WavelengthPath.CHOOSE_TEAM, element: <ChooseTeamPage /> },
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
