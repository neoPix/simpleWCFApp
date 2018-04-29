import Vue from 'vue';
import Router from 'vue-router';
import store from './store';
import UserList from './views/UserList.vue';
import UserCreate from './views/UserCreate.vue';
import UserEdit from './views/UserEdit.vue';
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
      path: '/create',
      name: 'userAdd',
      component: UserCreate,
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
      component: UserEdit,
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
