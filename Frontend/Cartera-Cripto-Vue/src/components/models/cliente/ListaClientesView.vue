<template>
  <!-- Componente de barra de navegación de clientes, siempre visible arriba -->
  <ClienteNavBar></ClienteNavBar>

  <!-- Contenedor de la tabla de clientes, con estilos de centrado y márgenes -->
  <div class="tableContainer max-w-5xl mx-auto mt-10 px-4">
    <h1>Lista de Clientes</h1>

    <!-- Permite scroll horizontal si la tabla es muy ancha -->
    <div class="overflow-x-auto">
      <table class="min-w-full bg-white border border-gray-200 shadow-md rounded-md overflow-hidden">
        <thead class="bg-gray-100 text-gray-700">
          <tr>
            <th class="py-3 px-4 text-left">ID</th>
            <th class="py-3 px-4 text-left">Nombre</th>
            <th class="py-3 px-4 text-left">Email</th>
           
          </tr>
        </thead>

        <!-- El cuerpo de la tabla se llena dinámicamente con los datos de clients -->
        <tbody>
          <!-- Por cada cliente en el array clients, se crea una fila de la tabla -->
          <tr v-for="client in clients" :key="client.id" class="hover:bg-gray-50">
            <td class="py-3 px-4 border-t">{{ client.id }}</td>
            <td class="py-3 px-4 border-t">{{ client.name }}</td>
            <td class="py-3 px-4 border-t">{{ client.email }}</td>
            
          </tr>
        </tbody>
      </table>
    </div>
  </div>

</template>

<script setup>
// Importa el componente de barra de navegación de clientes
import ClienteNavBar from './ClienteNavBar.vue';
// Importa la función ref de Vue para variables reactivas
import { ref } from 'vue';

// Define un array reactivo para almacenar los clientes
const clients = ref([]);

// Función asíncrona para cargar los datos desde la API
async function cargarDatosApi() {
  // Realiza una petición GET a la API de clientes
  let respuesta = await fetch('https://localhost:7273/api/Cliente');
  // Convierte la respuesta a JSON y la asigna al array clients
  clients.value = await respuesta.json();
}
// Llama a la función para cargar los datos al iniciar el componente
cargarDatosApi();
</script>

<style scoped>
.tableContainer {
  max-width: 70vw;
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
  td:nth-of-type(2):before { content: "Nombre"; }
  td:nth-of-type(3):before { content: "Email"; }
}
</style>