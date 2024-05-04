import { makeAutoObservable, reaction, runInAction } from "mobx";
import User from "../models/User";
import api from "../api/api";
import { StoreValue } from "./storeValue";
import { store } from "./store";

export default class UserStore {
  token: string | null = window.localStorage.getItem("jwt");

  userStoreValue = new StoreValue<User>();

  loadingOffStart = true;

  constructor() {
    makeAutoObservable(this);

    reaction(
      () => this.token,
      (token) => {
        if (token) {
          window.localStorage.setItem("jwt", token);
        } else {
          window.localStorage.removeItem("jwt");
        }
      }
    );
  }

  reset = () => {
    this.setToken(null);
    this.userStoreValue.setValue(undefined);
  };

  setToken = (token: string | null) => {
    this.token = token;
  };

  login = async (
    email: string,
    password: string
  ): Promise<User | undefined> => {
    return this.userStoreValue.handleAPICall(async () => {
      const user = await api.Account.login(email, password);

      if (user.token) {
        this.userStoreValue.setValue(user);
        this.setToken(user.token);

        if (user.activeGameSession) {
          store.gameSessionStore.gameSessionStoreValue.setValue(
            user.activeGameSession
          );
        }
      }

      return user;
    });
  };

  register = async (username: string, email: string, password: string, avatarId: string): Promise<User | undefined> => {
    return this.userStoreValue.handleAPICall(async () => {
      const user = await api.Account.register(username, email, password, avatarId);

      if (user.token) {
        this.userStoreValue.setValue(user);
        this.setToken(user.token);

        if (user.activeGameSession) {
          store.gameSessionStore.gameSessionStoreValue.setValue(
            user.activeGameSession
          );
        }
      }

      return user;
    });
  };

  currentUser = async () => {
    if (this.userStoreValue.value) return this.userStoreValue.value;

    const resolveLoadingOffStart = () =>
      runInAction(() => (this.loadingOffStart = false));

    const result = await this.userStoreValue.handleAPICall(async () => {
      if (!this.token) {
        resolveLoadingOffStart();
        return undefined;
      }

      // Ensure that the token isn't expired. If it is, the user should be logged out.
      const decode = JSON.parse(atob(this.token.split(".")[1]));
      if (decode.exp * 1000 < new Date().getTime()) {
        window.localStorage.removeItem("jwt");
        resolveLoadingOffStart();
        return undefined;
      }

      const user = await api.Account.getCurrentUser();

      if (user) {
        this.userStoreValue.setValue(user);

        if (user.activeGameSession) {
          store.gameSessionStore.gameSessionStoreValue.setValue(
            user.activeGameSession
          );
        }
      }

      resolveLoadingOffStart();

      return user;
    });

    return result;
  };

  logout = async () => {
    this.reset();
  };
}
