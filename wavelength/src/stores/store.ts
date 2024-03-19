import { createContext, useContext } from "react";
import UserStore from "./userStore";

class Store {
  userStore = new UserStore();
}

export const store: Store = new Store();

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext);
}
