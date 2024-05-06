import { useNavigate } from "react-router";
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

// import the avatar assigned to the user
import { avatarDict } from "../../utils/avatarUtils";

const NavBar: React.FC = observer(() => {
  const { userStore, gameSessionStore } = useStore();
  const [user] = useStoreValue(userStore.userStoreValue);
  const [game] = useStoreValue(gameSessionStore.gameSessionStoreValue);
  const navigate = useNavigate();
  // TODO: SET UP GAME SESSION
  // WHEN STORES ARE COMPLETE
  const isAuthenticated = !!user;
  const { isMobile } = useWindowSize();
  const [showProfileDropdown, setShowProfileDropdown] =
    useState<boolean>(false);
  // If the user logs our or logs in, make sure the profile dropdown isn't showing from prior usage.
  useEffect(() => {
    setShowProfileDropdown(false);
  }, [user]);
  // Copies the text of the game code to the user's clipboard
  const handleJoinCodeButtonClick = () => {
    if (game) navigator.clipboard.writeText(game.joinCode);
  };
  // Navigate to the login page
  const handleLoginButtonClick = () => {
    navigate(WavelengthPath.LOGIN);
  };
  // Navigate to the register page
  const handleRegisterButtonClick = () => {
    navigate(WavelengthPath.REGISTER);
  };

  const handleProfileButtonClick = () => {
    setShowProfileDropdown(!showProfileDropdown);
  };

  const showGameSessionDetails = isAuthenticated && !!game;
  const joinCodeButton = showGameSessionDetails && (
    <NavBarButton
      content={`GAME #${game.joinCode}`}
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
