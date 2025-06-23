<template>
  <!-- Barra de navegación para la sección de transacciones, siempre visible arriba -->
<TransaccionNavBar></TransaccionNavBar>

<!-- Contenedor principal de la tabla de historial de transacciones -->
<div class="tableContainer">
  <!-- Título del historial -->
  <H1>HISTORIAL DE TRANSACCIONES</H1>

  <!-- Tabla que muestra la lista de transacciones -->
  <table class="min-w-full bg-white border border-gray-200 shadow-md rounded-md">
  <!-- Encabezado de la tabla con los nombres de las columnas -->
  <tr class="bg-gray-100 text-center">
    <th class="py-3 px-5 border-b">ID</th>
    <th class="py-3 px-5 border-b">Action</th>
    <th class="py-3 px-5 border-b">Crypto code</th>
    <th class="py-3 px-5 border-b">Client id</th>
    <th class="py-3 px-5 border-b">Cantidad</th>
    <th class="py-3 px-5 border-b">Monto</th>
    <th class="py-3 px-5 border-b">Fecha</th>

  </tr>
  <!-- Cuerpo de la tabla: se llena dinámicamente con cada transacción -->
  <tbody>
  <!-- Por cada transacción, se crea una fila con sus datos --> 
    <tr v-for="transaction in transactions"
            :key="transaction.id" class="hover:bg-gray-50">
            <td class="py-3 px-5 border-b"> {{ transaction.id }} </td>
            <td class="py-3 px-5 border-b"> {{ transaction.action }} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.crypto_code}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.clienteId}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.crypto_amount}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.money}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.datetime}} </td>

    </tr>
  </tbody>

  </table>

</div>


</template>

<script setup>
// Importa el componente de barra de navegación de transacciones
import TransaccionNavBar from './TransaccionNavBar.vue';
// Importa ref para variables reactivas, las variables reactivas son aquellas que se actualizan automáticamente en la vista cuando cambian
import { ref } from 'vue';

// Array reactivo para almacenar las transacciones obtenidas de la API
const transactions = ref([]);

// Función asíncrona para cargar los datos de la API
async function cargarDatosApi() {
  // Realiza la petición GET a la API de transacciones
  let respuesta = await fetch('https://localhost:7273/api/Transaccion');
  // Convierte la respuesta a JSON y la asigna al array reactivo
  transactions.value = await respuesta.json();
}
// Llama a la función para cargar los datos automáticamente al iniciar el componente
cargarDatosApi();

</script>
<style scoped>
.tableContainer {
  max-width: 90vw;
  margin: 2rem auto;
  padding: 2rem;
  background: #f9fafb;
  border-radius: 16px;
  box-shadow: 0 6px 24px rgba(0,0,0,0.08);
}

h1 {
  color: #007bff;
  text-align: center;
  margin-bottom: 2rem;
  letter-spacing: 1px;
}

table {
  width: 100%;
  border-collapse: collapse;
  background: #fff;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 2px 8px rgba(0,0,0,0.04);
}

th, td {
  padding: 1rem 0.8rem;
  text-align: center;
}

th {
  background: #f1f5f9;
  color: #222;
  font-weight: 600;
  font-size: 1.05rem;
  border-bottom: 2px solid #e5e7eb;
}

td {
  border-bottom: 1px solid #e5e7eb;
  font-size: 1rem;
  color: #333;
}

tr:last-child td {
  border-bottom: none;
}

tr:hover {
  background: #f0f8ff;
  transition: background 0.2s;
}

@media (max-width: 900px) {
  .tableContainer {
    padding: 1rem;
  }
  th, td {
    padding: 0.7rem 0.3rem;
    font-size: 0.95rem;
  }
}

@media (max-width: 600px) {
  table, thead, tbody, th, td, tr {
    display: block;
  }
  th {
    display: none;
  }
  tr {
    margin-bottom: 1.2rem;
    background: #fff;
    border-radius: 8px;
    box-shadow: 0 1px 4px rgba(0,0,0,0.05);
    padding: 0.5rem;
  }
  td {
    border: none;
    position: relative;
    padding-left: 50%;
    text-align: left;
    min-height: 2.2rem;
  }
  td:before {
    position: absolute;
    left: 0.8rem;
    top: 50%;
    transform: translateY(-50%);
    font-weight: bold;
    color: #007bff;
    white-space: nowrap;
  }
  td:nth-of-type(1):before { content: "ID"; }
  td:nth-of-type(2):before { content: "Acción"; }
  td:nth-of-type(3):before { content: "Cripto"; }
  td:nth-of-type(4):before { content: "Cliente ID"; }
  td:nth-of-type(5):before { content: "Cantidad"; }
  td:nth-of-type(6):before { content: "Dinero"; }
  td:nth-of-type(7):before { content: "Fecha"; }
}
</style>