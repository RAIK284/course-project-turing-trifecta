type NavBarMobileProps = {
  loginButton?: React.ReactNode;
  registerButton?: React.ReactNode;
  joinCodeButton?: React.ReactNode;
  profileButton?: React.ReactNode;
};

/**
 * The mobile display of the navbar.
 */
const NavBarMobile: React.FC<NavBarMobileProps> = ({
  loginButton,
  registerButton,
  joinCodeButton,
  profileButton,
}) => (
  <nav className="NavBar Mobile flex flex-col p-1">
    <div className="uppercase text-3xl w-full text-center">Wavelength</div>
    <div className="absolute right-3 top-1">{profileButton}</div>
    {registerButton && loginButton && (
      <div className="flex gap-1 justify-center mt-2">
        {registerButton}
        {loginButton}
      </div>
    )}
    {joinCodeButton && (
      <div className="flex justify-center mt-2">{joinCodeButton}</div>
    )}
  </nav>
);

export default NavBarMobile;
