import {useNavigate} from 'react-router-dom';
import { WavelengthPath } from '../routing/Routes';

const RulesPage: React.FC = () => {
  
  const navigate = useNavigate();

  const returnToAuthenticatedPage = () => {
    navigate(WavelengthPath.LANDING);
  };
return (
  <div className="flex flex-col items-center justify-start h-screen overflow-y-auto px-4">
    <div className="w=full mb-4"></div>
      <button className="inline-block h-10 p-2 rounded-lg text-2xl text-white font-regular whitespace-nowrap mb-4" style={{ textShadow: '0 0 5px #ffffff' }} onClick={returnToAuthenticatedPage}>GO BACK</button>
      <div className="max-w-4xl text-center">
        <ul className="text-left text-white text-2xl font-semibold list-disc pl-4 leading-normal">
          <li>The goal: Work together to turn the dial as close to the center of a colored target as you can</li>
          <li>The twist: The target is completely hidden and is in a random location each round.</li>
          <li>Fortunately, one of your teammates is Psychic and knows exactly where the target is</li>
          <li>But they can only give a clue on a spectrum between two opposing concepts. For example, if the spectrum is hot to cold, and the target is on the hot side, you might give the clue, "coffee". Which is hot, but not the hottest thing imaginable.</li>
          <li>Every round you'll get a clue like this written by one of your friends, it is up to the selector to choose where on the spectrum this clue falls.</li>
          <li>Fear not, though, your teammates can show you where they would guess.</li>
          <li>The closer to the center you are, the more points you score!</li>
          <li>The game is completely cooperative, so you win or lose as a team.</li>
        </ul>
      </div>
    </div>
);
};


  
export default RulesPage;