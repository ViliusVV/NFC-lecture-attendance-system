import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import LoginPage from './views/LoginPage.vue';

Vue.use(Router);

export const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/home',
      name: 'home',
      component: Home,
    },
    {
      path: '/',
      name: 'login',
      component: LoginPage,
    },
    {
      path: '/device-fetch',
      name: 'device-fetch',
      component: () => import(/* webpackChunkName: "fetch-data" */ './views/DeviceFetch.vue'),
    },
    {
      path: '/fetch-data',
      name: 'fetch-data',
      component: () => import(/* webpackChunkName: "fetch-data" */ './views/FetchData.vue'),
    },
    {
      path: '/nfc-fetch',
      name: 'nfc-fetch',
      component: () => import(/* webpackChunkName: "fetch-data" */ './views/NfcFetch.vue'),
    },
    {
      path: '/user-list',
      name: 'user-list',
      component: () => import(/* webpackChunkName: "fetch-data" */ './views/UsersFetch.vue'),
    },
    {
      path: '/getusers',
      name: 'getusers',
      component: () => import(/* webpackChunkName: "fetch-data" */ './views/UserList.vue'),
    },
    {
      path: '/timetable',
      name: 'timetable',
      component: () => import(/* webpackChunkName: "fetch-data" */ './views/Timetable.vue'),
    },
    {
      path: '/statistics',
      name: 'statistics',
      component: () => import(/* webpackChunkName: "fetch-data" */ './views/Statistics.vue'),
    },
    {
      path: '*',
      redirect: '/home',
    },
  ],
});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/'];
  const authRequired = !publicPages.includes(to.path);
  let loggedIn = localStorage.getItem('user');

  if (loggedIn) {
    const dateNow = new Date().toJSON().slice(0, 19);
    const index = loggedIn.indexOf('expiration');
    const expiration = loggedIn.substring(index + 13, index + 32);
    if (expiration <= dateNow) {
      localStorage.removeItem('user');
      loggedIn = null;
      router.push('/');
    }
  }

  if (authRequired && !loggedIn) {
    return next({
      path: '/',
      query: { returnUrl: to.path },
    });
  }
  next();
});
