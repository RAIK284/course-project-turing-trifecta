import { useNavigate } from "react-router-dom";
import { WavelengthPath } from "../routing/Routes";
import { useStore } from "../stores/store";
import { useStoreValue } from "../stores/storeValue";
import { useState } from "react";
import { observer } from "mobx-react-lite";

const AuthenticatedLandingPage: React.FC = observer(() => {
  const navigate = useNavigate();
  const { gameSessionStore, userStore } = useStore();
  const { create: createGameSession, join } = gameSessionStore;
  const [user] = useStoreValue(userStore.userStoreValue);
  const [gameSessionCode, setGameSessionCode] = useState<string>("");
  const [, , error] = useStoreValue(gameSessionStore.gameSessionStoreValue);

  // This condition will never be true.
  if (!user) return <></>;

  const createSession = () => {
    if (user) {
      createGameSession(user.id);
    }
  };

  const handleRulesPageButtonClick = () => {
    navigate(WavelengthPath.RULES);
  };

  const handleGameSessionCodeInputUpdate = (newValue: string) => {
    const value = newValue.replace("#", "");
    if (value.length > 6) {
      return;
    }

    setGameSessionCode(value);
  };

  const handleJoinButtonClick = () => {
    join(user?.id, gameSessionCode);
  };

  return (
    <div className="h-screen flex flex-col justify-between items-center overflow-y-auto">
      <div className="w-1/3 my-auto space-y-4 flex flex-col items-center">
        <div className="w-full text-center">
          <text
            className="text-2xl font-regular text-center"
            style={{ textShadow: "0 0 5px #ffffff" }}
          >
            JOIN SESSION
          </text>
          <div className="flex gap-1">
            <input
              value={gameSessionCode ? `#${gameSessionCode}` : undefined}
              onChange={(e) => handleGameSessionCodeInputUpdate(e.target.value)}
              type="text"
              placeholder="ENTER GAME SESSION ID"
              className="w-full h-10 p-2 bg-cover-blue rounded-lg text-white text-center placeholder-white"
            />
            <button
              className="rounded-lg bg-cover-blue px-5"
              disabled={gameSessionCode.length !== 6}
              onClick={handleJoinButtonClick}
            >
              JOIN
            </button>
          </div>
          {error && <span className="text-center-red">{error}</span>}
        </div>
        <div className="w-full flex items-center">
          <hr className="flex-grow border-t-2 border-gray-300" />
          <span
            className="mx-4 text-gray-500 font-bold text-2xl"
            style={{ textShadow: "0 0 5px #ffffff" }}
          >
            or
          </span>
          <hr className="flex-grow border-t-2 border-gray-300" />
        </div>
        <div className="w-full">
          <button
            className="w-full h-10 p-2 bg-target-2 rounded-lg text- text-center"
            onClick={createSession}
          >
            CREATE SESSION
          </button>
          <div className="absolute bottom-5 left-5">
            <button
              onClick={handleRulesPageButtonClick}
              className="w-full h-10 text-bold p-2 bg-target-3 rounded-lg text-white text-center"
              style={{ textShadow: "0 0 5px #ffffff" }}
            >
              HOW TO PLAY
            </button>
          </div>
        </div>
      </div>
    </div>
  );
});

export default AuthenticatedLandingPage;
