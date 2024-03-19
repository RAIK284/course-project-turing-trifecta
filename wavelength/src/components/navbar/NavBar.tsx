import { useNavigate } from "react-router";
import GameSession from "../../models/GameSession";
import NavBarButton from "./NavBarButton";
import { WavelengthPath } from "../../routing/Routes";
import NavBarMobile from "./NavBarMobile";
import NavBarDesktop from "./NavBarDesktop";
import useWindowSize from "../../hooks/useWindowSize";
import UserCircle from "../../assets/icons/UserCircleIcon";
import { useEffect, useState } from "react";
import ProfileDropdown from "./ProfileDropdown";
import { useStore } from "../../stores/store";
import { useStoreValue } from "../../stores/storeValue";
import { observer } from "mobx-react-lite";

const NavBar: React.FC = observer(() => {
  const { userStore } = useStore();
  const [user] = useStoreValue(userStore.userStoreValue);
  const navigate = useNavigate();
  // TODO: SET UP GAME SESSION
  // WHEN STORES ARE COMPLETE
  const isAuthenticated = !!user;
  const gameSession = {
    joinCode: "123456",
  } as GameSession;
  const { isMobile } = useWindowSize();
  const [showProfileDropdown, setShowProfileDropdown] =
    useState<boolean>(false);

  // If the user logs out or logs in, make sure the profile dropdown isn't showing from prior usage.
  useEffect(() => {
    setShowProfileDropdown(false);
  }, [user]);

  // Copies the text of the game code to the user's clipboard
  const handleJoinCodeButtonClick = () => {
    navigator.clipboard.writeText(gameSession.joinCode);
  };

  // Navigates the user to the login page
  const handleLoginButtonClick = () => {
    navigate(WavelengthPath.LOGIN);
  };

  // Navigates the user to the register page
  const handleRegisterButtonClick = () => {
    navigate(WavelengthPath.REGISTER);
  };

  const handleProfileButtonClick = () => {
    setShowProfileDropdown(!showProfileDropdown);
  };

  const showGameSessionDetails = isAuthenticated && !!gameSession;
  const joinCodeButton = showGameSessionDetails && (
    <NavBarButton
      content={`GAME #${gameSession.joinCode}`}
      onClick={handleJoinCodeButtonClick}
      filled={true}
    />
  );
  const loginButton = !isAuthenticated && (
    <NavBarButton
      content="Login"
      filled={false}
      onClick={handleLoginButtonClick}
    />
  );
  const registerButton = !isAuthenticated && (
    <NavBarButton
      content="Register"
      filled={true}
      onClick={handleRegisterButtonClick}
    />
  );
  const profileButton = isAuthenticated && (
    <div>
      <button onClick={handleProfileButtonClick} type="button">
        <UserCircle />
      </button>
      {showProfileDropdown && <ProfileDropdown user={user} />}
    </div>
  );

  return isMobile ? (
    <NavBarMobile
      joinCodeButton={joinCodeButton}
      loginButton={loginButton}
      registerButton={registerButton}
      profileButton={profileButton}
    />
  ) : (
    <NavBarDesktop
      joinCodeButton={joinCodeButton}
      loginButton={loginButton}
      registerButton={registerButton}
      profileButton={profileButton}
    />
  );
});

export default NavBar;
