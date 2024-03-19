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
      <div className="grid grid-rows-4 gap-y-6 h-2/3 w-1/3 my-auto">
        <input className="bg-theme-blue border border-target-2 text-center" placeholder="Email" onChange={handleEmailChange}></input>
        <input className="bg-theme-blue border border-target-4 text-center" placeholder="Username" onChange={handleEmailChange}></input>
        <input className="bg-theme-blue border border-target-3 text-center" placeholder="Password" min={8} onChange={handlePasswordChange}></input>
        <input className="bg-theme-blue border border-target-3 text-center" placeholder="Confirm Password" min={8} onChange={handleConfirmChange}></input>
        <button className="bg-cover-blue h-16 rouded" onClick={handleRegisterClick}>Register</button>
      </div>
    </div>
  );
};

export default RegisterPage;
