import axios from "axios";

axios.defaults.baseURL = '/api';

const responseBody = (response) => response.data;

axios.interceptors.response.use((response) => response, 
    (error) => {
        let message = "";
        const {status, data} = error.response;
        switch (true) {
            case 400 === status:
                if (typeof data === 'string' ) {
                    message = data;
                }
                else
                {
                    message = 'bad request'
                }
                break;
            case 404 === status:
                message = 'not found';
                break;
            case 500 >= status:
                message = 'server error';
                break;
        }
        return Promise.reject(message);
    }
)

const Movies = {
    get: (id) => axios.get('/movies/' + id).then(responseBody),
    getList: () => axios.get('/movies/').then(responseBody),
    post: (movie) => axios.post('/movies/', movie).then(responseBody),
    put: (movie) => axios.put('/movies/' + movie.id, movie).then(responseBody),
    delete: (id) => axios.delete('/movies/' + id).then(responseBody),
    getExternalApi: () => axios.get('/movies/externalapi').then(responseBody),
}

const agent = {
    Movies, 
}

export default agent;