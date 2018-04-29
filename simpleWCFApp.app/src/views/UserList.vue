<template>
  <div>
    <el-button @click.prevent="$router.push({ name: 'userAdd' })" type="primary" icon="el-icon-plus" circle></el-button>
    <el-table :data="users">
      <el-table-column prop="id" label="#id"></el-table-column>
      <el-table-column prop="userName" label="UserName"></el-table-column>
      <el-table-column prop="login" label="Login"></el-table-column>
      <el-table-column
        fixed="right"
        label="Operations"
        width="120">
        <template slot-scope="scope">
          <el-button type="primary" icon="el-icon-edit" @click.prevent="goTo(scope.$index)" circle></el-button>
          <el-button type="danger" icon="el-icon-delete" @click.prevent="remove(scope.$index)" circle></el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>
<script>
import { mapState } from "vuex";
import Userservice from "../services/UserService";

export default {
  computed: mapState(["users"]),
  methods: {
    remove(index) {
      Userservice.remove(this.users[index]);
    },
    goTo(index) {
      this.$router.push({
        name: "userEdit",
        params: { id: this.users[index].id }
      });
    }
  }
};
</script>
