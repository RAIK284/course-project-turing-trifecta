import { useNavigate } from "react-router";
import { WavelengthPath } from "../routing/Routes";
import React, { ChangeEvent, useState } from 'react';
// import {account} from "../api/api"


const RegisterPage: React.FC = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [confirm, setConfirm] = useState("")



  const handleEmailChange = (event: ChangeEvent<HTMLInputElement>) => {
    setEmail(event.target.value);
  };

  const handlePasswordChange = (event: ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  }

  const handleConfirmChange = (event: ChangeEvent<HTMLInputElement>) => {
    setConfirm(event.target.value);
  }

  function handleRegisterClick(){
    if(password == confirm){
      navigate(WavelengthPath.LANDING);
    }
  }


  return(
    <div className="h-screen flex justify-center self-center space-y-1/2">
      <div className="grid grid-rows-4 gap-y-6 h-1/2 w-1/3 my-auto">
        <input className="bg-theme-blue border-2 border-target-2 text-center w-full h-10 p-2 rounded-lg placeholder-white text-white" placeholder="EMAIL" onChange={handleEmailChange}></input>
        <input className="bg-theme-blue border-2 border-target-4 text-center w-full h-10 p-2 rounded-lg placeholder-white text-white" placeholder="USERNAME" onChange={handleEmailChange}></input>
        <input className="bg-theme-blue border-2 border-target-3 text-center w-full h-10 p-2 rounded-lg placeholder-white text-white" placeholder="PASSWORD" min={8} onChange={handlePasswordChange}></input>
        <input className="bg-theme-blue border-2 border-target-3 text-center w-full h-10 p-2 rounded-lg placeholder-white text-white" placeholder="CONFIRM PASSWORD" min={8} onChange={handleConfirmChange}></input>
        <button className="bg-cover-blue h-11 rounded-lg md:text-2xl" onClick={handleRegisterClick} style={{textShadow: "0 0 5px #ffffff"}}>REGISTER</button>
      </div>
    </div>
  );
};

export default RegisterPage;
