import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import AuthenticatedRoute from "./AuthenticatedRoute";
import AuthenticatedLandingPage from "../pages/AuthenticatedLandingPage";
import UnauthenticatedLandingPage from "../pages/UnauthenticatedLandingPage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import ChooseTeamPage from "../pages/ChooseTeamPage";

export enum WavelengthPath {
  LANDING = "/",
  LOGIN = "/login",
  REGISTER = "/register",
  CHOOSE_TEAM = "/chooseTeam/:gameSessionID", // Choosing a team occurs in a game session, so we need to access the game's ID
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
        path: WavelengthPath.REGISTER,
        element: <RegisterPage />,
      },
      { path: WavelengthPath.CHOOSE_TEAM, element: <ChooseTeamPage /> },
    ],
  },
]);

export default router;
