import AuthHttp from './AuthHttpService';
import Userservice from './UserService';
import store from '../store';
import router from '../router';

class AuthService {
    async authenticate(login, password) {
        const { data: token } = await AuthHttp.post('auth', { login, password });
        store.commit('SET_TOKEN', token);
        router.replace({ name: 'home' });
        Userservice.init(token).loadUsers();
    }
}

export default new AuthService;