import {
  HubConnectionBuilder,
  HubConnection,
  LogLevel,
} from "@microsoft/signalr";
import { store } from "../stores/store";

class GameSessionHub {
  connection: HubConnection | undefined;

  gameSessionId: string | undefined;

  private createConnection = (gameSessionId: string): boolean => {
    if (this.connection && gameSessionId === this.gameSessionId) return false;
    console.log(this.connection, this.gameSessionId, gameSessionId);

    this.gameSessionId = gameSessionId;

    const token = store.userStore.token;

    if (!token) return false;

    this.connection = new HubConnectionBuilder()
      .withUrl(
        `${import.meta.env.APP_API_URL}/hub/gameSession?gameSessionId=${
          this.gameSessionId
        }`,
        {
          accessTokenFactory: () => token,
          withCredentials: false,
        }
      )

      .configureLogging(LogLevel.Information)
      .build();

    return true;
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
