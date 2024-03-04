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
  <nav className="NavBar Mobile flex flex-col">
    <div className="uppercase text-[2.5rem] w-full text-center font-light">
      Wavelength
    </div>
    <div className="absolute right-4 top-2.5">{profileButton}</div>
    {registerButton && loginButton && (
      <div className="flex gap-1 justify-center">
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
