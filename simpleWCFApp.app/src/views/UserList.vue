<template>
  <ul>
    <li v-for="user in users" :key="user.id">
      <router-link :to="{ name: 'userEdit', params: { id: user.id } }">{{ user.userName }}</router-link>
    </li>
  </ul>
</template>
<script>
import { mapState } from 'vuex';

export default {
  computed: mapState(['users'])
}
</script>
