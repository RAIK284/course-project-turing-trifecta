type NavBarDesktopProps = {
  loginButton?: React.ReactNode;
  registerButton?: React.ReactNode;
  joinCodeButton?: React.ReactNode;
  profileButton?: React.ReactNode;
};

/**
 * The desktop display of the navigation bar.
 */
const NavBarDesktop: React.FC<NavBarDesktopProps> = ({
  loginButton,
  registerButton,
  joinCodeButton,
  profileButton,
}) => (
  <nav className="NavBar Mobile flex flex-row w-full items-center p-1">
    <div className="w-1/3">{joinCodeButton}</div>
    <div className="uppercase text-3xl w-full text-center w-1/3">
      Wavelength
    </div>
    <div className="w-1/3 flex justify-end gap-1">
      {registerButton}
      {loginButton}
      {profileButton}
    </div>
  </nav>
);

export default NavBarDesktop;
