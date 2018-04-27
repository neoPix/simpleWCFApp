import UserHttp from './UserHttpService';
import store from '../store';

class UserService {
    init(token) {
        UserHttp.defaults.headers.Authorization = token;
        return this;
    }
    async loadUsers() {
        const { data: { items: users } } = await UserHttp.get('/users');
        store.commit('SET_USERS', users);
    }
}

export default new UserService;