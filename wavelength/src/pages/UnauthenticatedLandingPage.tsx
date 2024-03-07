import { useNavigate } from "react-router";
import { WavelengthPath } from "../routing/Routes";



const UnauthenticatedLandingPage: React.FC = () => {
  const navigate = useNavigate();

  function handleLoginClick(){
    navigate(WavelengthPath.LOGIN);
  }

  function handleRegisterClick(){
    navigate(WavelengthPath.REGISTER);
  }

  return(
    <div className="h-screen flex justify-center self-center space-y-1/2">
      <div className="grid grid-rows h-1/3 w-1/3 my-auto">
        <button className="bg-cover-blue h-16" onClick={handleLoginClick}>Login</button>
        <p className="justify-self-center">or</p>
        <button className="bg-target-2 h-16" onClick={handleRegisterClick}>Register</button>
      </div>
    </div>
  );
};

export default UnauthenticatedLandingPage;
