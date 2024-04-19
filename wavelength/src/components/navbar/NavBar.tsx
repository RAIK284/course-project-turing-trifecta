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
  const isAuthenticated = !!user;
  const gameSession = {
    joinCode: "123456",
  } as GameSession;
  const { isMobile } = useWindowSize();
  const [showProfileDropdown, setShowProfileDropdown] =
    useState<boolean>(false);

  useEffect(() => {
    setShowProfileDropdown(false);
  }, [user]);

  const handleJoinCodeButtonClick = () => {
    navigator.clipboard.writeText(gameSession.joinCode);
  };

  const handleLoginButtonClick = () => {
    navigate(WavelengthPath.LOGIN);
  };

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
