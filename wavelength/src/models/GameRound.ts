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
};
