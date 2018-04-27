import Vue from 'vue';
import Router from 'vue-router';
import store from './store';
import UserList from './views/UserList.vue';
import Login from './views/Login.vue';

Vue.use(Router)

const router = new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: UserList,
      beforeEnter(from, to, next){
        if(store.state.token === null) {
          return next({ name: 'login' });
        }
        next();
      }
    },
    {
      path: '/:id',
      name: 'userEdit',
      component: UserList,
      beforeEnter(from, to, next){
        if(store.state.token === null) {
          return next({ name: 'login' });
        }
        next();
      }
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    }
  ],
  scrollBehavior (to, from, savedPosition) {
  }
});

export default router;
