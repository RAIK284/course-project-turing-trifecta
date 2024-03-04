import axios, { AxiosRequestConfig } from "axios";
import User from "../models/User";

axios.defaults.baseURL = import.meta.env.APP_API_URL;

axios.interceptors.request.use((config) => {
  const token: string = ""; // TODO: USE STORE TO GET TOKEN
  console.log("making a request, but the token isnt set yet"); // TODO: REMOVE THIS WHEN TOKEN IS SET

  if (token && config && config.headers)
    config.headers.Authorization = `Bearer ${token}`;
  return config;
});

const requests = {
  get: <T>(
    url: string,
    params: URLSearchParams = new URLSearchParams()
  ): Promise<T> =>
    axios.get<T>(url, { params }).then((response) => response.data),
  post: <T>(
    url: string,
    body: unknown,
    config?: AxiosRequestConfig<unknown> | undefined
  ): Promise<T> =>
    axios.post<T>(url, body, config).then((response) => response.data),
  put: <T>(url: string, body: unknown): Promise<T> =>
    axios.put<T>(url, body).then((response) => response.data),
  delete: <T>(url: string, body: unknown): Promise<T> =>
    axios.delete<T>(url, { data: body }).then((response) => response.data),
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

const api = {
  Account,
};

export default api;
