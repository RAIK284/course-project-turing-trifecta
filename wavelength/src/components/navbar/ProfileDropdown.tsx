import React, { useState } from "react";
import User from "../../models/User";
import { useStore } from "../../stores/store";
import {avatarDict} from "../../utils/avatarUtils";

type ProfileDropdownProps = {
  user: User;
};

const ProfileDropdown: React.FC<ProfileDropdownProps> = ({ user }) => {
  const { userStore } = useStore();
  const [editedUser, setEditedUser] = useState<User>({ ...user });
  const [isAvatarPopupOpen, setIsAvatarPopupOpen] = useState(false);
  const [isUsernameDropdownOpen, setIsUsernameDropdownOpen] = useState(false);
  const [isPasswordDropdownOpen, setIsPasswordDropdownOpen] = useState(false);
  const [password, setPassword] = useState("");

  const updateUserValue = (key: keyof User, value: unknown) => {
    setEditedUser({ ...editedUser, [key]: value });
  };

  const saveEditedUser = () => {
    userStore.updateProfile(editedUser);
  }

  const saveEditedPassword = () => {
    userStore.changePassword(password);
  }

  const handleLogout = () => {
    userStore.logout();
  };

  const handleUsernameEditClick = () => {
    setIsUsernameDropdownOpen(!isUsernameDropdownOpen);
  };

  const handlePasswordEditClick = () => {
    setIsPasswordDropdownOpen(!isPasswordDropdownOpen);
  };

  const handleCloseAvatarPopup = () => {
    setIsAvatarPopupOpen(false);
  };

  const handleOpenAvatarPopup = () => {
    setIsAvatarPopupOpen(true);
  };

  return (
    <div className="relative">
      <div className="dropdown-content absolute bg-scoreboard-blue p-3 top-105% right-5 w-fit rounded-md flex flex-col gap-1 items-center" onClick={(e) => e.stopPropagation()}>
        <div className="dropdown-section ">
          <button className="edit-button bg-center-red px-3 py-1 mt-3 mx-auto block rounded-lg font-bold whitespace-nowrap" onClick={handleUsernameEditClick}>
            Edit Username
          </button>
          {isUsernameDropdownOpen && (
            <div className="dropdown-content font-bold">
              <label className="username-label">New Username</label>
              <input
                className="username-input text-black rounded-sm p-1"
                onChange={(e) => updateUserValue("userName", e.target.value)}
                value={editedUser.userName}
              />
              <button className="save-button" disabled={user.userName === editedUser.userName} onClick={saveEditedUser}>
                Save Changes
              </button>
            </div>
          )}
          <button className="edit-button  bg-center-red px-3 py-1 mt-3 mx-auto block rounded-lg font-bold whitespace-nowrap" onClick={handlePasswordEditClick}>
            Edit Password
          </button>
          {isPasswordDropdownOpen && (
            <div className="dropdown-content font-bold">
              <label className="password-label">New Password</label>
              <input
                className="password-input text-black rounded-sm p-1"
                onChange={(e) => setPassword(e.target.value)}
                value={password}
              />
              <button className="save-button" disabled={password.length === 0} onClick={saveEditedPassword}>
                Save Changes
              </button>
            </div>
          )}
          <button className="edit-button bg-center-red px-3 py-1 mt-3 mx-auto block rounded-lg font-bold whitespace-nowrap" onClick={handleOpenAvatarPopup}>
            Edit Avatar
          </button>
          {isAvatarPopupOpen && (
            <div className="avatar-popup">
              <div className="grid grid-cols-3 gap-2 mt-4">
                {Object.keys(avatarDict).map((key) => (
                  <img
                    key={key}
                    src={avatarDict[key] as string}
                    alt="avatar"
                    className="avatar w-20 h-20 cursor-pointer hover:border-4 border-blue-500"
                    onClick={ async () => {
                      await userStore.updateProfile({ ...editedUser, avatarId: key });
                      handleCloseAvatarPopup();
                    }}
                  />
                ))}
              </div>
              <button className="cancel-button font-bold block mx-auto" onClick={handleCloseAvatarPopup}>
                Cancel
              </button>
            </div>
          )}
        </div>
        <button className="logout-button  bg-center-red px-3 py-1 mt-3 mx-auto block rounded-lg font-bold whitespace-nowrap" onClick={handleLogout}>
          Logout
        </button>
      </div>
    </div>
  );
};

export default ProfileDropdown;
