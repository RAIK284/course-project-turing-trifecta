import { makeAutoObservable } from "mobx";

export class StoreValue<T> {
  /**
   * The value this StoreValue holds. Undefined by default.
   */
  value: T | undefined;

  isLoading = false;

  error: string | undefined;

  constructor() {
    makeAutoObservable(this);
  }

  setValue = (newValue: T | undefined) => {
    this.value = newValue;
  };

  setLoading = (isLoading: boolean) => {
    this.isLoading = isLoading;
  };

  setError = (error: string | undefined) => {
    this.error = error;
  };

  /**
   * Handles API calling logic by wrapping it in a try/catch block and automatically handling loading and error logic.
   * @param logic the logic to be done with the API call. Should call the API, return the data fetched from the API, and update the store value.
   * @returns the value that was retrieved in the fetch method.
   */
  handleAPICall = async (
    logic: () => Promise<T | undefined>
  ): Promise<T | undefined> => {
    // If the store value is already loading, we don't want to call the API again
    if (this.isLoading) {
      return undefined;
    }

    try {
      this.setLoading(true);
      const result = await logic();
      this.setLoading(false);
      this.setError(undefined);
      return result;
    } catch (error) {
      if (error instanceof Error) {
        this.setError(error.message);
      }
      this.setLoading(false);
      return undefined;
    }
  };
}

export function useStoreValue<T>(storeValue: StoreValue<T>) {
  return [storeValue.value, storeValue.isLoading, storeValue.error] as const;
}
