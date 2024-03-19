import { useState } from "react";
import User from "../../models/User";
import { useStore } from "../../stores/store";

type ProfileDropdownProps = {
  user: User;
};

const ProfileDropdown: React.FC<ProfileDropdownProps> = ({ user }) => {
  const { userStore } = useStore();
  const [editedUser, setEditedUser] = useState<User>({ ...user });

  const updateUserValue = (key: keyof User, value: unknown) => {
    setEditedUser({ ...editedUser, [key]: value });
  };

  const handleLogoutButtonClick = () => {
    userStore.logout();
  };

  const canSaveChanges = editedUser.userName !== user.userName;

  return (
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
      </div>
      <button
        className="bg-center-red px-3 py-1 rounded-sm uppercase font-bold mt-5 w-fit text-sm"
        onClick={handleLogoutButtonClick}
      >
        Logout
      </button>
    </div>
  );
};

export default ProfileDropdown;