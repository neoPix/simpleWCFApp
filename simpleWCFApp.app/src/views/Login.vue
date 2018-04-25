<template>
  <div class="app-auth">
      <login/>
  </div>
</template>

<script>
import Login from '../components/Login.vue';

export default {
  components: {
      'login': Login
  }
}
</script>
