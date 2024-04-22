import {
  HubConnectionBuilder,
  HubConnection,
  LogLevel,
} from "@microsoft/signalr";
import { store } from "../stores/store";

class GameSessionHub {
  connection: HubConnection | undefined;

  gameSessionId: string | undefined;

  private createConnection = (gameSessionId: string) => {
    if (this.connection && gameSessionId === this.gameSessionId) return;

    this.gameSessionId = gameSessionId;

    const token = store.userStore.token;

    if (!token) return;

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
  };

  connect = async (gameSessionId: string) => {
    this.createConnection(gameSessionId);

    if (!this.connection) return;

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
