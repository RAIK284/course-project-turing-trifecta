import { makeAutoObservable, reaction, runInAction } from "mobx";
import User from "../models/User";
import api from "../api/api";
import { StoreValue } from "./storeValue";

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
      }

      return user;
    });
  };

  register = async (newUser: User): Promise<User | undefined> => {
    return this.userStoreValue.handleAPICall(async () => {
      const user = await api.Account.register(newUser);

      if (user.token) {
        this.userStoreValue.setValue(user);
        this.setToken(user.token);
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

      const user = await api.Account.getCurrentUser();

      if (user) {
        this.userStoreValue.setValue(user);
      }

      resolveLoadingOffStart();

      return user;
    });

    return result;
  };

  logout = async () => {
    this.setToken(null);
    this.userStoreValue.setValue(undefined);
  };
}
