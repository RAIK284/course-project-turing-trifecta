import { makeAutoObservable, runInAction } from "mobx";
import { StoreValue } from "./storeValue";
import GameSession, { getCurrentGameRound } from "../models/GameSession";
import api from "../api/api";
import Team from "../models/Team";
import gameSessionHub from "../signalR/gameSessionHub";
import GameSessionMember from "../models/GameSessionMember";
import { deepCopy } from "../utils/utils";
import { AxiosError } from "axios";
import { store } from "./store";

export default class GameSessionStore {
  gameSessionStoreValue = new StoreValue<GameSession>();

  callingEndpoint = false;

  constructor() {
    makeAutoObservable(this);

    gameSessionHub.observe("UserSwitchedTeams", (member: GameSessionMember) => {
      if (!this.gameSessionStoreValue.value) return;

      const gameSession = deepCopy(this.gameSessionStoreValue.value);
      const memberIndex = gameSession.members.findIndex(
        (m) => m.userId === member.userId
      );

      runInAction(() => {
        if (memberIndex === -1) gameSession.members.push(member);
        else gameSession.members[memberIndex] = member;

        this.gameSessionStoreValue.setValue(gameSession);
      });
    });

    gameSessionHub.observe(
      "UserJoinedGameSession",
      (member: GameSessionMember) => {
        if (!this.gameSessionStoreValue.value) return;

        const gameSession = deepCopy(this.gameSessionStoreValue.value);
        const memberIndex = gameSession.members.findIndex(
          (m) => m.userId === member.userId
        );

        runInAction(() => {
          if (memberIndex === -1) gameSession.members.push(member);
          else gameSession.members[memberIndex] = member;

          this.gameSessionStoreValue.setValue(gameSession);
        });
      }
    );

    gameSessionHub.observe("RoundStarted", (gameSession: GameSession) => {
      this.gameSessionStoreValue.setValue(gameSession);
    });
  }

  reset = () => {
    this.gameSessionStoreValue.setValue(undefined);
  };

  create = async (ownerId: string) => {
    await this.gameSessionStoreValue.handleAPICall(async () => {
      const result = await api.GameSessions.create(ownerId);

      if (result) {
        this.gameSessionStoreValue.setValue(result);
      }

      return result;
    });
  };

  join = async (userId: string, joinCode: string) => {
    await this.gameSessionStoreValue.handleAPICall(async () => {
      const result = await api.GameSessions.join(userId, joinCode);

      if (result) {
        this.gameSessionStoreValue.setValue(result);
      }

      return result;
    });
  };

  start = async (gameSessionId: string) => {
    await this.gameSessionStoreValue.handleAPICall(async () => {
      const result = await api.GameSessions.start(gameSessionId);

      if (result) {
        this.gameSessionStoreValue.setValue(result);
      }

      return result;
    });
  };

  get = async (gameSessionId: string) => {
    await this.gameSessionStoreValue.handleAPICall(async () => {
      const result = await api.GameSessions.get(gameSessionId);

      if (result) {
        this.gameSessionStoreValue.setValue(result);
      }

      return result;
    });
  };

  switchTeams = async (userId: string, gameSessionId: string, team: Team) => {
    const { value: activeGameSession } = this.gameSessionStoreValue;

    if (!activeGameSession) return undefined;

    try {
      runInAction(() => (this.callingEndpoint = true));

      const member = await api.GameSessions.switchTeams(
        userId,
        gameSessionId,
        team
      );

      // Add/update the member to the game's list of members
      if (member) {
        const newMembers = [...activeGameSession.members];
        const indexOfMember = newMembers.findIndex(
          (gsm) => gsm.id === member.id
        );
        if (indexOfMember === -1) newMembers.push(member);
        else newMembers[indexOfMember] = member;

        const newGame = { ...activeGameSession, members: newMembers };
        this.gameSessionStoreValue.setValue(newGame);
      }

      runInAction(() => (this.callingEndpoint = false));
    } catch (e) {
      if (e instanceof Error) {
        this.gameSessionStoreValue.setError(e.message);
      }
      runInAction(() => (this.callingEndpoint = false));
    }
  };

  giveClue = async (gameSessionId: string, clue: string) => {
    const { value: activeGameSession } = this.gameSessionStoreValue;

    if (!activeGameSession) return undefined;

    runInAction(() => (this.callingEndpoint = true));
    try {
      const roundResult = await api.GameRounds.giveClue(gameSessionId, clue);

      const newRounds = activeGameSession.rounds;
      const roundIndex = newRounds.findIndex((r) => r.id === roundResult.id);

      if (roundIndex !== -1) {
        newRounds[roundIndex] = roundResult;
      }

      this.gameSessionStoreValue.setValue({
        ...activeGameSession,
        rounds: newRounds,
      });
    } catch (e) {
      if (e instanceof AxiosError) {
        this.gameSessionStoreValue.setError(e.response?.data);
      } else if (e instanceof Error) {
        this.gameSessionStoreValue.setError(e.message);
      }
    }

    runInAction(() => (this.callingEndpoint = false));
  };

  selectorSelect = async (gameSessionId: string, targetOffset: number) => {
    const { value: activeGameSession } = this.gameSessionStoreValue;
    const { value: user } = store.userStore.userStoreValue;

    if (!activeGameSession || !user) return undefined;

    runInAction(() => (this.callingEndpoint = true));
    try {
      const selectorSelection = await api.GameRounds.selectTarget(
        gameSessionId,
        user.id,
        targetOffset
      );

      const mostRecentRound = getCurrentGameRound(activeGameSession);

      if (mostRecentRound) {
        runInAction(() => {
          mostRecentRound.selectorSelection = selectorSelection;
          this.gameSessionStoreValue.setValue({ ...activeGameSession });
        });
      }
    } catch (e) {
      if (e instanceof AxiosError) {
        this.gameSessionStoreValue.setError(e.response?.data);
      } else if (e instanceof Error) {
        this.gameSessionStoreValue.setError(e.message);
      }
    }

    runInAction(() => (this.callingEndpoint = false));
  };

  ghostGuess = async (gameSessionId: string, targetOffset: number) => {
    const { value: activeGameSession } = this.gameSessionStoreValue;
    const { value: user } = store.userStore.userStoreValue;

    if (!activeGameSession || !user) return undefined;

    runInAction(() => (this.callingEndpoint = true));
    try {
      const ghostGuess = await api.GameRounds.performGhostGuess(
        gameSessionId,
        user.id,
        targetOffset
      );

      const mostRecentRound = getCurrentGameRound(activeGameSession);

      if (mostRecentRound) {
        const newGhostGuesses = [
          ...(mostRecentRound.ghostGuesses ?? []),
          ghostGuess,
        ];
        runInAction(() => {
          mostRecentRound.ghostGuesses = newGhostGuesses;
          this.gameSessionStoreValue.setValue({ ...activeGameSession });
        });
      }
    } catch (e) {
      if (e instanceof AxiosError) {
        this.gameSessionStoreValue.setError(e.response?.data);
      } else if (e instanceof Error) {
        this.gameSessionStoreValue.setError(e.message);
      }
    }

    runInAction(() => (this.callingEndpoint = false));
  };

  opposingGhostGuess = async (gameSessionId: string, isLeft: boolean) => {
    const { value: activeGameSession } = this.gameSessionStoreValue;
    const { value: user } = store.userStore.userStoreValue;

    if (!activeGameSession || !user) return undefined;

    runInAction(() => (this.callingEndpoint = true));
    try {
      const ghostGuess = await api.GameRounds.performOpposingTeamGuess(
        gameSessionId,
        user.id,
        isLeft
      );

      const mostRecentRound = getCurrentGameRound(activeGameSession);

      if (mostRecentRound) {
        const newGhostGuesses = [
          ...(mostRecentRound.opposingGhostGuesses ?? []),
          ghostGuess,
        ];
        runInAction(() => {
          mostRecentRound.opposingGhostGuesses = newGhostGuesses;
          this.gameSessionStoreValue.setValue({ ...activeGameSession });
        });
      }
    } catch (e) {
      if (e instanceof AxiosError) {
        this.gameSessionStoreValue.setError(e.response?.data);
      } else if (e instanceof Error) {
        this.gameSessionStoreValue.setError(e.message);
      }
    }

    runInAction(() => (this.callingEndpoint = false));
  };

  opposingSelectorSelect = async (gameSessionId: string, isLeft: boolean) => {
    const { value: activeGameSession } = this.gameSessionStoreValue;
    const { value: user } = store.userStore.userStoreValue;

    if (!activeGameSession || !user) return undefined;

    runInAction(() => (this.callingEndpoint = true));
    try {
      const opposingSelectorSelect =
        await api.GameRounds.performOpposingTeamSelection(
          gameSessionId,
          user.id,
          isLeft
        );

      const mostRecentRound = getCurrentGameRound(activeGameSession);

      if (mostRecentRound) {
        runInAction(() => {
          mostRecentRound.opposingTeamSelection = opposingSelectorSelect;
          this.gameSessionStoreValue.setValue({ ...activeGameSession });
        });
      }
    } catch (e) {
      if (e instanceof AxiosError) {
        this.gameSessionStoreValue.setError(e.response?.data);
      } else if (e instanceof Error) {
        this.gameSessionStoreValue.setError(e.message);
      }
    }

    runInAction(() => (this.callingEndpoint = false));
  };
}
