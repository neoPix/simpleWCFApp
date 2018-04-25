import UserHttp from './UserHttpService';

class AuthService {
    async authenticate(login, password) {
        const { data: result } = await UserHttp.post('auth', { login, password });
        console.log(result);
    }
}

export default new AuthService;