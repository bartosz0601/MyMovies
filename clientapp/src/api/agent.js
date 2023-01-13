import axios from "axios";

axios.defaults.baseURL = '/api';

const responseBody = (response) => response.data;

const Movies = {
    get: (id) => axios.get('/movies/' + id).then(responseBody),
    getList: () => axios.get('/movies/').then(responseBody),
    post: (movie) => axios.post('/movies/', movie).then(responseBody),
    put: (movie) => axios.put('/movies/' + movie.id, movie).then(responseBody),
    delete: (id) => axios.delete('/movies/' + id).then(responseBody)
}

const agent = {
    Movies, 
}

export default agent;