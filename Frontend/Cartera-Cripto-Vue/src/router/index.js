// Importa las funciones principales de vue-router
import { createRouter, createWebHistory } from 'vue-router'

// Importa los componentes de vistas principales y secundarias
import HomeView from '../views/HomeView.vue'

import ClientesView from '@/views/ClientesView.vue'
import NuevoClienteView from '@/components/models/cliente/NuevoClienteView.vue'
import ListaClientesView from '@/components/models/cliente/ListaClientesView.vue'

import TransaccionesView from '@/views/TransaccionesView.vue'
import NuevaTransaccionView from '@/components/models/transaccion/NuevaTransaccionView.vue'
import ListaTransaccionesView from '@/components/models/transaccion/ListaTransaccionesView.vue'

// Crea el router con historial HTML5 y define las rutas de la aplicación
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL), // Usa el base URL del proyecto
  routes: [
    {
      path: '/', // Ruta principal (inicio)
      name: 'home',
      component: HomeView,
    },
    {
  path: '/clientes',             // Ruta en la barra de direcciones: /clientes
  name: 'clientes',              // Nombre interno de la ruta (puedes usarlo para navegación programática)
  component: ClientesView        // Componente que se renderiza cuando accedes a /clientes
},
{
  path: '/transacciones',        // Ruta: /transacciones 
  name: 'transacciones',         // Nombre de la ruta para navegación
  component: TransaccionesView   // Componente que se renderiza al acceder a /transacciones
},
    {
      path: '/clientes/new', // Ruta para crear un cliente nuevo
      name: 'nuevocliente',
      component: NuevoClienteView
    },
    {
      path: '/clientes/list', // Ruta para ver la lista de clientes
      name: 'listaclientes',
      component: ListaClientesView
    },
    {
      path: '/transacciones/new', // Ruta para crear una nueva transacción
      name: 'nuevatransaccion',
      component: NuevaTransaccionView
    },
    {
      path: '/transacciones/list', // Ruta para ver el historial de transacciones
      name: 'listatransacciones',
      component: ListaTransaccionesView
    },
  ],
})

// Exporta el router para ser usado en main.js
export default router