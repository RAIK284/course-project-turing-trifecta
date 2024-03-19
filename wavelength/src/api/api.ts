import axios, { AxiosRequestConfig, AxiosResponse } from "axios";
import User from "../models/User";
import { store } from "../stores/store";

axios.defaults.baseURL = import.meta.env.APP_API_URL + "/api";

axios.interceptors.request.use((config) => {
  const { token } = store.userStore;

  if (token && config && config.headers)
    config.headers.Authorization = `Bearer ${token}`;
  return config;
});

const handleResponse = (axiosResponse: AxiosResponse) => {
  if (axiosResponse.status === 200) {
    return axiosResponse.data;
  }

  throw new Error(axiosResponse.data);
};

const requests = {
  get: <T>(
    url: string,
    params: URLSearchParams = new URLSearchParams()
  ): Promise<T> => axios.get<T>(url, { params }).then(handleResponse),
  post: <T>(
    url: string,
    body: unknown,
    config?: AxiosRequestConfig<unknown> | undefined
  ): Promise<T> => axios.post<T>(url, body, config).then(handleResponse),
  put: <T>(url: string, body: unknown): Promise<T> =>
    axios.put<T>(url, body).then(handleResponse),
  delete: <T>(url: string, body: unknown): Promise<T> =>
    axios.delete<T>(url, { data: body }).then(handleResponse),
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
