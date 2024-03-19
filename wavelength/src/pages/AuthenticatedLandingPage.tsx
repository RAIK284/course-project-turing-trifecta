import { useNavigate } from 'react-router-dom';
import { WavelengthPath } from '../routing/Routes';


const AuthenticatedLandingPage: React.FC = () => {
  
  const navigate = useNavigate();

  const createSession = () => {
    navigate(WavelengthPath.CHOOSE_TEAM);
  };

  return (
    <div className="h-screen flex justify-center items-center">
      <div className="w-1/3 my-auto space-y-4 flex flex-col items-center">
        <div className="w-full text-center">
          <text className="text-2xl font-regular text-center" style={{ textShadow: '0 0 5px #ffffff' }}>JOIN SESSION</text>
          <input type="text" placeholder="Enter Game Session ID" className="w-full h-10 p-2 bg-cover-blue rounded-lg text-white text-center placeholder-white" />
        </div>
        <div className="w-full flex items-center">
          <hr className="flex-grow border-t border-gray-300" />
          <span className="mx-2 text-gray-500 font-bold">or</span>
          <hr className="flex-grow border-t border-gray-300" />
        </div>
        <div className="w-full">
          <button className="w-full h-10 p-2 bg-target-2 rounded-lg text-black text-center" onClick={createSession}>CREATE SESSION</button>
          <div className="absolute bottom-5 left-5">
            <button className="w-full h-10 p-2 bg-target-3 rounded-lg text-black text-center">HOW TO PLAY</button>
          </div>
        </div>
      </div>
    </div>
  );
};


export default AuthenticatedLandingPage;
