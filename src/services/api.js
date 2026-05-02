import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5000/api', // Adjust to your .NET API URL
    headers: {
        'Content-Type': 'application/json'
    }
});

export default {
    getTasks(status, search) {
        return api.get('/tasks', { params: { status, search } });
    },
    getTask(id) {
        return api.get(`/tasks/${id}`);
    },
    createTask(task) {
        return api.post('/tasks', task);
    },
    updateTask(id, task) {
        return api.put(`/tasks/${id}`, task);
    },
    deleteTask(id) {
        return api.delete(`/tasks/${id}`);
    }
};
