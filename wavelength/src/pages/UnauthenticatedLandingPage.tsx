const UnauthenticatedLandingPage: React.FC = () => {
  function handleLoginClick(){
    console.log("Login")
  }

  function handleRegisterClick(){
    console.log("Register")
  }

  return(
    <div className="bg-theme-blue">
      <button className="bg-cover-blue" onClick={handleLoginClick}>Login</button>
      <button className="bg-target-2" onClick={handleRegisterClick}>Register</button>
    </div>
  );
};

export default UnauthenticatedLandingPage;
