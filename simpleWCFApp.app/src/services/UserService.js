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
    async remove(user) {
        await UserHttp.delete(user.url);
        await this.loadUsers();
    }
    async add(user) {
        const usedUser = {
            ...user,
            id: null
        }
        await UserHttp.post(`/users`, {user: usedUser});
        await this.loadUsers();
    }
    async upgrade(user) {
        await UserHttp.put(user.url, {user});
        await this.loadUsers();
    }
    async get(id) {
        const { data: user } = await UserHttp.get(`/users/${id}`);
        return user;
    }
}

export default new UserService;