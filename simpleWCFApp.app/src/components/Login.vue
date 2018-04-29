<template>
  <el-form @submit.native.prevent="submit()">
      <el-form-item label="Username">
          <el-input type="text" v-model="userName"/>
      </el-form-item>
      <el-form-item label="Password">
          <el-input type="password" v-model="password"/>
      </el-form-item>
      <el-button type="primary" native-type="submit">
          Login
      </el-button>
  </el-form>
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

