import { createContext, useContext } from "react";
import UserStore from "./userStore";
import GameSessionStore from "./gameSessionStore";

class Store {
  userStore = new UserStore();
  gameSessionStore = new GameSessionStore();
}

export const store: Store = new Store();

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext);
}
