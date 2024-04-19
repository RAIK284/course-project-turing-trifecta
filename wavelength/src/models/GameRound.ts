import { GhostGuess } from "./GhostGuess";
import { OpposingGhostGuess } from "./OpposingGhostGuess";
import { OpposingSelectorSelection } from "./OpposingSelectorSelection";
import { RoundRole } from "./RoundRole";
import { SelectorSelection } from "./SelectorSelection";
import { SpectrumCard } from "./SpectrumCard";
import Team from "./Team";

export type GameRound = {
  id: string;
  gameSessionId: string;
  teamTurn: Team.ONE | Team.TWO;
  spectrumCardId: string;
  spectrumCard: SpectrumCard;
  clue: string;
  targetOffset: number;
  ghostGuesses?: GhostGuess[];
  roundRoles?: RoundRole[];
  selectorSelection?: SelectorSelection;
  opposingGhostGuesses?: OpposingGhostGuess[];
  opposingTeamSelection?: OpposingSelectorSelection;
};
