import axios, { AxiosRequestConfig, AxiosResponse } from "axios";
import User from "../models/User";
import { store } from "../stores/store";
import GameSession from "../models/GameSession";
import Team from "../models/Team";
import GameSessionMember from "../models/GameSessionMember";
import { GameRound } from "../models/GameRound";
import { SelectorSelection } from "../models/SelectorSelection";
import { GhostGuess } from "../models/GhostGuess";
import { OpposingGhostGuess } from "../models/OpposingGhostGuess";
import { OpposingSelectorSelection } from "../models/OpposingSelectorSelection";

axios.defaults.baseURL = import.meta.env.APP_API_URL + "/api";

axios.interceptors.request.use((config) => {
  const { token } = store.userStore;

  if (token && config && config.headers)
    config.headers.Authorization = `Bearer ${token}`;
  return config;
});

const handleResponse = (axiosResponse: AxiosResponse) => {
  if (axiosResponse.status === 200) {
    return axiosResponse.data;
  }

  throw new Error(axiosResponse.data);
};

const requests = {
  get: <T>(
    url: string,
    params: URLSearchParams = new URLSearchParams()
  ): Promise<T> => axios.get<T>(url, { params }).then(handleResponse),
  post: <T>(
    url: string,
    body: unknown,
    config?: AxiosRequestConfig<unknown> | undefined
  ): Promise<T> => axios.post<T>(url, body, config).then(handleResponse),
  put: <T>(url: string, body: unknown): Promise<T> =>
    axios.put<T>(url, body).then(handleResponse),
  delete: <T>(url: string, body: unknown): Promise<T> =>
    axios.delete<T>(url, { data: body }).then(handleResponse),
};

const Account = {
  login: (email: string, password: string) =>
    requests.post<User>(
      "/account/login",
      { email, password },
      { validateStatus: null }
    ),
  register: (user: User) => requests.post<User>("/account/register", user),
  getCurrentUser: () => requests.get<User>("/account"),
};

const GameSessions = {
  create: (ownerId: string) =>
    requests.post<GameSession>("/gameSessions/create", { ownerId }),
  start: (gameSessionId: string) =>
    requests.post<GameSession>("/gameSessions/start", { gameSessionId }),
  join: (userId: string, joinCode: string) =>
    requests.post<GameSession>("/gameSessions/join", { userId, joinCode }),
  get: (gameSessionId: string) =>
    requests.get<GameSession>(`/gameSessions/${gameSessionId}`),
  switchTeams: (userId: string, gameSessionId: string, team: Team) =>
    requests.post<GameSessionMember>("/gameSessions/switchTeams", {
      gameSessionId,
      team,
      userId,
    }),
};

const GameRounds = {
  giveClue: (gameSessionId: string, clue: string) =>
    requests.post<GameRound>("/gameRound/giveClue", {
      gameSessionId,
      clue,
    }),
  performGhostGuess: (
    gameSessionId: string,
    userId: string,
    targetOffset: number
  ) =>
    requests.post<GhostGuess>("/gameRound/performGhostGuess", {
      gameSessionId,
      userId,
      targetOffset: Math.round(targetOffset),
    }),
  selectTarget: (gameSessionId: string, userId: string, targetOffset: number) =>
    requests.post<SelectorSelection>("/gameRound/selectTarget", {
      gameSessionId,
      userId,
      targetOffset: Math.round(targetOffset),
    }),
  performOpposingTeamGuess: (
    gameSessionId: string,
    userId: string,
    isLeft: boolean
  ) =>
    requests.post<OpposingGhostGuess>("/gameRound/performOpposingTeamGuess", {
      gameSessionId,
      userId,
      isLeft,
    }),
  performOpposingTeamSelection: (
    gameSessionId: string,
    userId: string,
    isLeft: boolean
  ) =>
    requests.post<OpposingSelectorSelection>(
      "/gameRound/performOpposingTeamSelection",
      {
        gameSessionId,
        userId,
        isLeft,
      }
    ),
};

const api = {
  Account,
  GameSessions,
  GameRounds,
};

export default api;
