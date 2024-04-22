import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";
import { store } from "../stores/store";

class GameSessionHub {
  connection: HubConnection | undefined;

  gameSessionId: string | undefined;

  observers: Map<string, (data: unknown) => void> = new Map();

  private createConnection = (gameSessionId: string): boolean => {
    if (this.connection && gameSessionId === this.gameSessionId) return false;

    this.gameSessionId = gameSessionId;

    const token = store.userStore.token;

    if (!token) return false;

    const connection = new HubConnectionBuilder()
      .withUrl(
        `${import.meta.env.APP_API_URL}/hub/gameSession?gameSessionId=${
          this.gameSessionId
        }`,
        {
          accessTokenFactory: () => token,
        }
      )
      .withAutomaticReconnect()
      .build();

    this.observers.forEach((action, key) => {
      connection.on(key, (data) => {
        action(data);
      });
    });

    this.connection = connection;

    return true;
  };

  observe = <T extends object>(
    eventName: string,
    action: (data: T) => void
  ) => {
    this.observers.set(eventName, action as (data: unknown) => void);
  };

  connect = async (gameSessionId: string) => {
    if (!this.createConnection(gameSessionId) || !this.connection) return;

    try {
      await this.connection.start();
    } catch (err) {
      console.log(err);
    }
  };

  disconnect = () => {
    if (!this.connection) return;

    this.connection.stop().catch((error) => {
      console.log("Error disconnecting: ", error);
    });
    this.connection = undefined;
  };
}

const gameSessionHub = new GameSessionHub();

export default gameSessionHub;
