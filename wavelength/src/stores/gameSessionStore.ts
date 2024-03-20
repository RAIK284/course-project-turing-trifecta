import { makeAutoObservable, runInAction } from "mobx";
import { StoreValue } from "./storeValue";
import GameSession from "../models/GameSession";
import api from "../api/api";
import Team from "../models/Team";

export default class GameSessionStore {
  gameSessionStoreValue = new StoreValue<GameSession>();

  selectingTeam = false;

  constructor() {
    makeAutoObservable(this);
  }

  create = async (ownerID: string) => {
    await this.gameSessionStoreValue.handleAPICall(async () => {
      const result = await api.GameSessions.create(ownerID);

      if (result) {
        this.gameSessionStoreValue.setValue(result);
      }

      return result;
    });
  };

  join = async (userID: string, joinCode: string) => {
    await this.gameSessionStoreValue.handleAPICall(async () => {
      const result = await api.GameSessions.join(userID, joinCode);

      if (result) {
        this.gameSessionStoreValue.setValue(result);
      }

      return result;
    });
  };

  start = async (gameSessionID: string) => {
    await this.gameSessionStoreValue.handleAPICall(async () => {
      const result = await api.GameSessions.start(gameSessionID);

      if (result) {
        this.gameSessionStoreValue.setValue(result);
      }

      return result;
    });
  };

  get = async (gameSessionID: string) => {
    await this.gameSessionStoreValue.handleAPICall(async () => {
      const result = await api.GameSessions.get(gameSessionID);

      if (result) {
        this.gameSessionStoreValue.setValue(result);
      }

      return result;
    });
  };

  switchTeams = async (userID: string, gameSessionID: string, team: Team) => {
    const { value: activeGameSession } = this.gameSessionStoreValue;

    if (!activeGameSession) return undefined;

    try {
      runInAction(() => (this.selectingTeam = true));

      const member = await api.GameSessions.switchTeams(
        userID,
        gameSessionID,
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
