import { useNavigate } from 'react-router-dom';
import { WavelengthPath } from '../routing/Routes';
const LoginPage: React.FC = () => {

    const navigate = useNavigate();

    function handleLoginClick() {
        navigate(WavelengthPath.LOGIN);
    }

    return(
        <div className="h-screen flex justify-center items-center">
        <div className="w-1/3 my-auto space-y-4 flex flex-col items-center">
            <input type="text" placeholder="ENTER EMAIL" className="w-full h-10 p-2 bg-cover-blue rounded-lg text-white text-center placeholder-white" />
            <input type="password" placeholder="ENTER PASSWORD" className="w-full h-10 p-2 bg-target-2 rounded-lg text-white text-center placeholder-white" />
            <button className="h-15 md:text-2xl font-sans bg-blue-500 text-white rounded-lg px-4" onClick={handleLoginClick}>LOGIN</button>
        </div>
    </div>
    )
}

export default LoginPage;