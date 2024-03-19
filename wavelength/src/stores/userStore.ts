import { makeAutoObservable, reaction } from "mobx";
import User from "../models/User";
import api from "../api/api";
import { StoreValue } from "./storeValue";

export default class UserStore {
  token: string | null = window.localStorage.getItem("jwt");

  user = new StoreValue<User>();

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
    return this.user.handleAPICall(async () => {
      const user = await api.Account.login(email, password);

      if (user.token) {
        this.user.setValue(user);
        this.token = user.token;
      }

      return user;
    });
  };

  register = async (newUser: User): Promise<User | undefined> => {
    return this.user.handleAPICall(async () => {
      const user = await api.Account.register(newUser);

      if (user.token) {
        this.user.setValue(user);
        this.token = user.token;
      }

      return user;
    });
  };

  currentUser = async () => {
    return this.user.handleAPICall(async () => {
      const user = await api.Account.getCurrentUser();

      if (user.token) {
        this.user.setValue(user);
      }

      return user;
    });
  };

  logout = async () => {
    this.setToken(null);
    this.user.setValue(undefined);
  };
}
