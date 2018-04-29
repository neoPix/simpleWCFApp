<template>
  <user :isEditing="true" :user="user" v-if="user" />
</template>

<script>
import UserForm from "../components/UserForm.vue";
import Userservice from "../services/UserService";

export default {
  data() {
    return {
      user: null
    };
  },
  components: {
    user: UserForm
  },
  async created() {
      this.user = await Userservice.get(this.$route.params.id);
  }
};
</script>
