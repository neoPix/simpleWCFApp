<template>
  <form @submit.prevent.stop="submit">
      <label>
          userName:
          <input type="text" v-model="userName"/>
      </label>
      <label>
          password:
          <input type="password"  v-model="password"/>
      </label>
      <button class="login-validate" type="submit">
          Login
      </button>
  </form>
</template>

<script>
import AuthService from '../services/AuthService';

export default {
  data() {
      return {
          userName: 'user01',
          password: '123456789'
      }
  },
  methods: {
      async submit() {
          await AuthService.authenticate(this.userName, this.password);
      }
  }
}
</script>

