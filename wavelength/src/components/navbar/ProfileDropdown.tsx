import { useState } from "react";
import User from "../../models/User";
import { useStore } from "../../stores/store";
import avatar1 from "../../assets/avatars/avatar1.png";
import avatar2 from "../../assets/avatars/avatar2.png";
import avatar3 from "../../assets/avatars/avatar3.png";
import avatar4 from "../../assets/avatars/avatar4.png";
import avatar5 from "../../assets/avatars/avatar5.png";
import avatar6 from "../../assets/avatars/avatar6.png";


type ProfileDropdownProps = {
  user: User;
};

const ProfileDropdown: React.FC<ProfileDropdownProps> = ({ user }) => {
  const { userStore } = useStore();
  const [editedUser, setEditedUser] = useState<User>({ ...user });
  const [isAvatarPopupOpen, setIsAvatarPopupOpen] = useState(false);

  const updateUserValue = (key: keyof User, value: unknown) => {
    setEditedUser({ ...editedUser, [key]: value });
  };

  const handleLogoutButtonClick = () => {
    userStore.logout();
  };

  const handleAvatarButtonClick = () => {
    setIsAvatarPopupOpen(true);
  };

  const handleCloseAvatarPopup = () => {
    setIsAvatarPopupOpen(false);
  };

  const canSaveChanges = editedUser.userName !== user.userName;

  return (
  <div className="relative">
    <div
      className="absolute bg-scoreboard-blue p-3 top-105% right-5 w-fit rounded-md flex flex-col gap-1 items-center"
      onClick={(e) => e.stopPropagation()}
    >
      <div className="flex flex-col">
        <label className="w-fit uppercase text-xs font-bold">Username</label>
        <input
          className="text-black rounded-sm p-1"
          onChange={(e) => updateUserValue("userName", e.target.value)}
          value={editedUser.userName}
        />
        <button
          className="bg-theme-blue px-3 py-1 mt-3"
          disabled={!canSaveChanges}
        >
          Save Changes
        </button>
        <button
          className="bg-theme-blue px-3 py-1 mt-3"
          onClick={handleAvatarButtonClick}
        >
          Edit Avatar
        </button>

        {isAvatarPopupOpen && (
          <div className="avatar-popup">
             <div className="grid grid-cols-3 gap-2 mt-4">
                <img src={avatar1} alt="Avatar 1" className="w-24 h-20 cursor-pointer hover:border-4 border-blue-500" />
                <img src={avatar2} alt="Avatar 2" className="w-24 h-20 cursor-pointer hover:border-4 border-blue-500"/>
                <img src={avatar3} alt="Avatar 3" className="w-24 h-20 cursor-pointer hover:border-4 border-blue-500"/>
                <img src={avatar4} alt="Avatar 4" className="w-24 h-20 cursor-pointer hover:border-4 border-blue-500"/>
                <img src={avatar5} alt="Avatar 5" className="w-24 h-20 cursor-pointer hover:border-4 border-blue-500"/>
                <img src={avatar6} alt="Avatar 6" className="w-24 h-20 cursor-pointer hover:border-4 border-blue-500"/>
              </div>
            <button 
              className="bg-center-red px-3 py-1 mt-3 mx-auto block"
            onClick={handleCloseAvatarPopup}
            >Cancel</button>
          </div>
        )}
      </div>

      <button
        className="bg-center-red px-3 py-1 rounded-sm uppercase font-bold mt-5 w-fit text-sm"
        onClick={handleLogoutButtonClick}
      >
        Logout
      </button>
    </div>
  </div>
  );
};

export default ProfileDropdown;
