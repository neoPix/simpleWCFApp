<template>
  <user :isEditing="false" :user="user" />
</template>

<script>
import UserForm from "../components/UserForm.vue";

export default {
  data() {
    return {
      user: {
          userName: ""
      }
    };
  },
  components: {
    user: UserForm
  }
};
</script>
