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
    <div className="h-screen flex justify-center items-center space-y-1/2">
  <div className="grid grid-rows h-1/3 w-1/3 my-auto">
    <button className="bg-cover-blue rounded-lg h-16 md:text-3xl font-sans" style={{ textShadow: "0 0 5px #ffffff" }} onClick={handleLoginClick}>LOGIN</button>
    <div className="w-full flex items-center justify-center space-x-5">
      <hr className="flex-grow border-t-2 border-gray-300" />
      <span className="text-gray-500 text-3xl font-bold" style={{ textShadow: "0 0 5px #ffffff" }}>or</span>
      <hr className="flex-grow border-t-2 border-gray-300" />
    </div>
    <button className="bg-target-2 h-16 rounded-lg md:text-3xl font-sans mt-4" style={{ textShadow: "0 0 5px #ffffff" }} onClick={handleRegisterClick}>CREATE ACCOUNT</button>
  </div>
</div>
  );
};

export default UnauthenticatedLandingPage;
