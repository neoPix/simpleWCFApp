import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    token: null,
    users: []
  },
  mutations: {
    ['SET_TOKEN'](state, token) {
      state.token = token;
    },
    ['SET_USERS'](state, users) {
      state.users = users;
    }
  },
  actions: {

  }
})
