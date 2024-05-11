import { useNavigate } from "react-router";
import { WavelengthPath } from "../routing/Routes";
import React, { ChangeEvent, useState } from 'react';
import { useStore } from "../stores/store";
import { useStoreValue } from "../stores/storeValue";
import { observer } from "mobx-react-lite";

const RegisterPage: React.FC = observer(() => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("")
  const [username, setUsername] = useState("")
  const [password, setPassword] = useState("")
  const [confirm, setConfirm] = useState("")
  const { userStore } = useStore();
  const { register, userStoreValue } = userStore;
  const [, isLoading, error] = useStoreValue(userStoreValue);




  const handleEmailChange = (event: ChangeEvent<HTMLInputElement>) => {
    setEmail(event.target.value);
  };

  const handleUsernameChange = (event: ChangeEvent<HTMLInputElement>) => {
    setUsername(event.target.value);
  };

  const handlePasswordChange = (event: ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  }

  const handleConfirmChange = (event: ChangeEvent<HTMLInputElement>) => {
    setConfirm(event.target.value);
  }

  const handleRegisterSubmit = async () => {
    const user = await register(username, email, password, "8bae381d-ba6a-4522-8b4d-ab57ea0459bd");
    if(user){
      navigate(WavelengthPath.LANDING);
    }
  }


  return(
    <div className="h-screen flex justify-center self-center space-y-1/2">
      <form className="grid grid-rows-4 gap-y-6 h-1/2 w-1/3 my-auto" 
        onSubmit={(e) => {
          // Prevent page refresh from submit
          e.preventDefault();
          handleRegisterSubmit();
        }}
        autoComplete="on"
        >

        <input //Enter Email
        type="email"
        className="bg-theme-blue border-2 border-target-2 text-center w-full h-10 p-2 rounded-lg placeholder-white text-white" 
        placeholder="EMAIL" 
        onChange={handleEmailChange}>
        </input>

        <input //Enter Username
        className="bg-theme-blue border-2 border-target-4 text-center w-full h-10 p-2 rounded-lg placeholder-white text-white" 
        placeholder="USERNAME" 
        onChange={handleUsernameChange}>
        </input>

        <input //Enter Password
        type="password"
        className="bg-theme-blue border-2 border-target-3 text-center w-full h-10 p-2 rounded-lg placeholder-white text-white" 
        placeholder="PASSWORD" 
        min={8} 
        onChange={handlePasswordChange}>
        </input>

        <input //Confirm Password
        type="password"
        className="bg-theme-blue border-2 border-target-3 text-center w-full h-10 p-2 rounded-lg placeholder-white text-white" 
        placeholder="CONFIRM PASSWORD" 
        min={8} 
        onChange={handleConfirmChange}>
        </input>


        <button 
        type="submit" 
        className="bg-cover-blue h-11 rounded-lg md:text-2xl" 
        disabled={isLoading || !email || !username || !password || !confirm || password != confirm} 
        style={{textShadow: "0 0 5px #ffffff"}}>
        REGISTER
        </button>
        
        {error && <span className="text-center-red">{error}</span>}
      </form>
    </div>
  );
});

export default RegisterPage;
