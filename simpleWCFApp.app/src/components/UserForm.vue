<template>
  <el-form @submit.native.prevent="submit()">
      <el-form-item label="Username">
          <el-input type="text" v-model="user.userName"/>
      </el-form-item>
      <el-form-item label="Login">
          <el-input type="text" v-model="user.login"/>
      </el-form-item>
      <el-button type="primary" native-type="submit" v-if="isEditing === false">
          Create
      </el-button>
      <el-button type="primary" native-type="submit" v-else>
          Save
      </el-button>
  </el-form>
</template>

<script>
import Userservice from "../services/UserService";

export default {
  props: {
    isEditing: {
      type: Boolean,
      default: true
    },
    user: {
      type: Object
    }
  },
  methods: {
    async submit() {
      if (this.isEditing === false) {
        await Userservice.add(this.user);
      } else {
          await Userservice.upgrade(this.user);
      }
      this.$router.back();
    }
  }
};
</script>

