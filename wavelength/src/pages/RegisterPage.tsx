import { useNavigate } from "react-router";
import { WavelengthPath } from "../routing/Routes";
import User from "../models/User";
import React, { ChangeEvent, useState } from 'react';
// import {account} from "../api/api"
import { useStore } from "../stores/store";
import { useStoreValue } from "../stores/storeValue";


const RegisterPage: React.FC = () => {
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

  function handleRegisterClick(){
    //Still need to connect to backend and actually register the user
    // const user = await register();
    // if(user){
      // navigate(WavelengthPath.LANDING);
    // }

    navigate(WavelengthPath.LANDING);
  }


  return(
    <div className="h-screen flex justify-center self-center space-y-1/2">
      <form className="grid grid-rows-4 gap-y-6 h-1/2 w-1/3 my-auto" onClick={handleRegisterClick}>

        <input //Enter Email
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
        className="bg-theme-blue border-2 border-target-3 text-center w-full h-10 p-2 rounded-lg placeholder-white text-white" 
        placeholder="PASSWORD" 
        min={8} 
        onChange={handlePasswordChange}>
        </input>

        <input //Confirm Password
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
};

export default RegisterPage;
