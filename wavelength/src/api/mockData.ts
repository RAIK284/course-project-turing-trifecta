import { GameRound } from "../models/GameRound";
import Team from "../models/Team";

export const psychicGiveClueRound: GameRound = {
  id: "b3ef55de-d830-449a-a2d2-af75c910048b",
  clue: "Emily Tinky???",
  gameSessionId: "b3ef55de-d830-449a-a2d2-af75c910048c",
  spectrumCard: {
    id: "b3ef55de-d830-449a-a2d2-af75c910048e",
    leftName: "Risky",
    rightName: "Safe",
  },
  spectrumCardId: "b3ef55de-d830-449a-a2d2-af75c910048e",
  targetOffset: 90,
  teamTurn: Team.ONE,
};
