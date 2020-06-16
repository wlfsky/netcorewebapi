import Vue, { AsyncComponent } from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import Home from '../views/Home.vue';

const CategoryView: AsyncComponent = (): any => import('@/views/CategoryView.vue');
const IndexView: AsyncComponent = (): any => import('@/views/IndexView.vue');
const ContentView: AsyncComponent = (): any => import('@/views/ContentView.vue');
const ContentListView: AsyncComponent = (): any => import('@/views/ContentList.vue');
const AboutView: AsyncComponent = (): any => import('@/views/About.vue');
const AdView: AsyncComponent = (): any => import('@/components/AdView.vue');
const SingView: AsyncComponent = (): any => import('@/views/sing.vue');

Vue.use(VueRouter);

const base_routes: RouteConfig[] = [
  // {
  //   path: '/',
  //   name: 'Home',
  //   component: Home,
  // },
  // {
  //   path: '/about',
  //   name: 'About',
  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () => import(/* webpackChunkName: "about" */ '../views/About.vue'),
  // },
];

const new_routes: RouteConfig[] = [
  {
    path: '/',
    name: 'IndexView',
    component: IndexView
  },
  {
    path: '/Category/:cid',
    name: 'Category',
    component: CategoryView
  },
  {
    path: '/ContentView/:ContentId',
    name: 'ContentView',
    component: ContentView
  },
  {
    path: '/ContentListView/:CategoryId',
    name: 'ContentListView',
    component: ContentListView
  },
  {
    path: '/about',
    name: 'AboutView',
    component: AboutView
  },
  {
    path: '/sing',
    name: 'SingView',
    component: SingView
  },
];
//组合两个路由组
var routes = base_routes.concat(new_routes)

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
