import { GameRound } from "../models/GameRound";
import Team from "../models/Team";

export const psychicGiveClueRound: GameRound = {
  id: "b3ef55de-d830-449a-a2d2-af75c910048b",
  clue: "",
  gameSessionId: "b3ef55de-d830-449a-a2d2-af75c910048c",
  spectrumCard: {
    id: "b3ef55de-d830-449a-a2d2-af75c910048e",
    leftName: "Risky",
    rightName: "Safe",
  },
  spectrumCardId: "b3ef55de-d830-449a-a2d2-af75c910048e",
  targetOffset: 139,
  teamTurn: Team.ONE,
};

export const selectorSelectRound: GameRound = {
  id: "b3ef55de-d830-449a-a2d2-af75c910048b",
  clue: "Being Harith's Roommate",
  gameSessionId: "b3ef55de-d830-449a-a2d2-af75c910048c",
  spectrumCard: {
    id: "b3ef55de-d830-449a-a2d2-af75c910048e",
    leftName: "Risky",
    rightName: "Safe",
  },
  spectrumCardId: "b3ef55de-d830-449a-a2d2-af75c910048e",
  targetOffset: -1,
  teamTurn: Team.ONE,
  ghostGuesses: [
    {
      id: "b3ef55de-d830-449a-a2d2-af75c9100411",
      gameRoundId: "b3ef55de-d830-449a-a2d2-af75c9100412",
      gameSessionId: "b3ef55de-d830-449a-a2d2-af75c9100413",
      targetOffset: 81,
      team: Team.ONE,
      userId: "b3ef55de-d830-449a-a2d2-af75c9100414",
    },
    {
      id: "b3ef55de-d830-449a-a2d2-af75c9100412",
      gameRoundId: "b3ef55de-d830-449a-a2d2-af75c9100412",
      gameSessionId: "b3ef55de-d830-449a-a2d2-af75c9100413",
      targetOffset: 37,
      team: Team.ONE,
      userId: "b3ef55de-d830-449a-a2d2-af75c9100414",
    },
  ],
};
