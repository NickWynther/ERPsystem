import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home'

Vue.use(Router)

export default new Router({
    mode:'history',
    routes: [
        {
            path: '/',
            component: Home
        },
        {
            path: '/products',
            component: ()=> import('./views/Products.vue')
        },
        {
            path: '/authorization',
            component: ()=> import('./views/Authorization.vue')
        },
        {
            path: '/signin',
            component: ()=> import('@/components/auth/Signin.vue')
        }
    ]
})