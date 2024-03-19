import { useState } from "react";
import { useStore } from "../stores/store";
import { useStoreValue } from "../stores/storeValue";
import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router-dom";
import { WavelengthPath } from "../routing/Routes";
const LoginPage: React.FC = observer(() => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const { userStore } = useStore();
  const { login, userStoreValue } = userStore;
  const [, isLoading, error] = useStoreValue(userStoreValue);
  const navigate = useNavigate();

  const handleLoginClick = async () => {
    const user = await login(email, password);

    if (user) {
      navigate(WavelengthPath.LANDING);
    }
  };

  return (
    <div className="h-screen flex justify-center items-center">
      <div className="w-1/3 my-auto space-y-4 flex flex-col items-center">
        <input
          type="text"
          placeholder="ENTER EMAIL"
          className="w-full h-10 p-2 bg-cover-blue rounded-lg text-white text-center placeholder-white"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <input
          type="password"
          placeholder="ENTER PASSWORD"
          className="w-full h-10 p-2 bg-target-2 rounded-lg text-white text-center placeholder-white"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button
          className="h-15 md:text-2xl font-sans bg-blue-500 text-white rounded-lg px-4"
          onClick={handleLoginClick}
          disabled={isLoading || !email || !password}
        >
          LOGIN
        </button>
        {error && <span className="text-center-red">{error}</span>}
      </div>
    </div>
  );
});

export default LoginPage;
