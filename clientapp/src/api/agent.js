import axios from "axios";

axios.defaults.baseURL = "/api";

axios.interceptors.response.use(
  (response) => response.data,
  (error) => {
    let message = "";
    const {data, statusText, status } = error.response; 
    if (typeof data === "string" && status === 400) {
      message = data;
    } else {
      message = statusText;
    }
    return Promise.reject(message);
  }
);

const moviesUrl = "/movies/";
const Movies = {
  get: (id) => axios.get(moviesUrl + id),
  getList: () => axios.get(moviesUrl),
  post: (movie) => axios.post(moviesUrl, movie),
  put: (movie) => axios.put(moviesUrl + movie.id, movie),
  delete: (id) => axios.delete(moviesUrl + id),
  getExternalApi: () => axios.get(moviesUrl + "externalapi"),
};

const agent = {
  Movies,
};

export default agent;
