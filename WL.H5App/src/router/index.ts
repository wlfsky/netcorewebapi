// import Vue from 'vue'
// import Router from 'vue-router'
// import HelloWorld from '@/components/HelloWorld'
import Vue, { AsyncComponent } from 'vue'
import Router, { RouteConfig, Route, NavigationGuard } from 'vue-router'

const CategoryView: AsyncComponent = (): any => import('@/views/CategoryView.vue')
const IndexView: AsyncComponent = (): any => import('@/views/IndexView.vue')
const ContentView: AsyncComponent = (): any => import('@/views/ContentView.vue')
const ContentListView: AsyncComponent = (): any => import('@/views/ContentList.vue')
const AboutView: AsyncComponent = (): any => import('@/views/About.vue')
const AdView: AsyncComponent = (): any => import('@/components/AdView.vue')
const SingView: AsyncComponent = (): any => import('@/views/sing.vue')

Vue.use(Router)

// export default new Router({
//   routes: [
//     {
//       path: '/',
//       name: 'HelloWorld',
//       component: HelloWorld
//     }
//   ]
// })

const routes: RouteConfig[] = [
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
]

const router: Router = new Router({
  mode: 'history',
  base: '/',
  routes
})

export default router
