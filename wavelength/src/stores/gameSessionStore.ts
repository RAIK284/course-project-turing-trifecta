import { makeAutoObservable, runInAction } from "mobx";
import { StoreValue } from "./storeValue";
import GameSession from "../models/GameSession";
import api from "../api/api";
import Team from "../models/Team";
import gameSessionHub from "../signalR/gameSessionHub";
import GameSessionMember from "../models/GameSessionMember";
import { deepCopy } from "../utils/utils";

export default class GameSessionStore {
  gameSessionStoreValue = new StoreValue<GameSession>();

  selectingTeam = false;

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
      runInAction(() => (this.selectingTeam = true));

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

      runInAction(() => (this.selectingTeam = false));
    } catch (e) {
      if (e instanceof Error) {
        this.gameSessionStoreValue.setError(e.message);
      }
      runInAction(() => (this.selectingTeam = false));
    }
  };
}
