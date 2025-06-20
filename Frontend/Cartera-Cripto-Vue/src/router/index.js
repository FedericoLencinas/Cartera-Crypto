import { createRouter, createWebHistory } from 'vue-router'

import HomeView from '../views/HomeView.vue'

import ClientesView from '@/views/ClientesView.vue'
import NuevoClienteView from '@/components/models/cliente/NuevoClienteView.vue'
import ListaClientesView from '@/components/models/cliente/ListaClientesView.vue'

import TransaccionesView from '@/views/TransaccionesView.vue'
import NuevaTransaccionView from '@/components/models/transaccion/NuevaTransaccionView.vue'
import ListaTransaccionesView from '@/components/models/transaccion/ListaTransaccionesView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    }
    ,
    {
      path: '/clientes',
      name: 'clientes',
      component: ClientesView
    },
    {
      path: '/transacciones',
      name: 'transacciones',
      component: TransaccionesView
    },
    {
      path: '/clientes/new',
      name: 'nuevocliente',
      component: NuevoClienteView
    },
    {
      path: '/clientes/list',
      name: 'listaclientes',
      component: ListaClientesView
    },
    {
      path: '/transacciones/new',
      name: 'nuevatransaccion',
      component: NuevaTransaccionView
    },
    {
      path: '/transacciones/list',
      name: 'listatransacciones',
      component: ListaTransaccionesView
    },
  ],
})

export default router