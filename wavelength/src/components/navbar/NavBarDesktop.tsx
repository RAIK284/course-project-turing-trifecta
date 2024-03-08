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
  <nav className="NavBar Mobile flex flex-row w-full items-center px-5 h-fit">
    <div className="w-1/3">{joinCodeButton}</div>
    <div className="uppercase text-[2.5rem] w-1/3 font-light text-center h-fit">
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
