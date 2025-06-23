<template>
  <!-- Barra de navegación de clientes, siempre visible arriba -->
  <ClienteNavBar></ClienteNavBar>

  <!-- Contenedor del formulario para crear un nuevo cliente -->
  <div class="crear-cliente-container">
    <!-- Formulario de registro, ejecuta registrarCliente al enviar -->
    <form class="crear-cliente-form" @submit.prevent="registrarCliente">
      <h2>Crear Cliente</h2>
      <!-- Campo para el nombre -->
      <div class="form-group">
        <label for="nombre">Nombre</label>
        <input v-model="nombre" id="nombre" placeholder="Nombre" required />
      </div>
      <!-- Campo para el email -->
      <div class="form-group">
        <label for="email">Email</label>
        <input v-model="email" id="email" placeholder="Email" type="email" required />
      </div>
      <!-- Campo para la contraseña -->
      <div class="form-group">
        <label for="password">Contraseña</label>
        <input v-model="password" id="password" placeholder="Contraseña" type="password" required />
      </div>
      <!-- Botón para enviar el formulario -->
      <button type="submit" class="btn-registrar">Registrar</button>
      <!-- Mensaje de éxito o error, aparece sólo si mensaje tiene valor -->
      <div v-if="mensaje" class="mensaje">{{ mensaje }}</div>
    </form>
  </div>
</template>

<script setup>
// Importa el componente de barra de navegación de clientes
import ClienteNavBar from './ClienteNavBar.vue';
// Importa ref para variables reactivas y axios para peticiones HTTP
import { ref } from 'vue'
import axios from 'axios'

// Variables reactivas para los campos del formulario
const nombre = ref('')
const email = ref('')
const password = ref('')
// Variable reactiva para mostrar mensajes al usuario
const mensaje = ref('')

// Función asíncrona para registrar un nuevo cliente
const registrarCliente = async () => {
  try {
    // Envía los datos del formulario a la API para registrar el cliente
    const res = await axios.post('https://localhost:7273/api/Cliente/register', {
      name: nombre.value,
      email: email.value,
      password: password.value
    })
    // Si todo sale bien, muestra mensaje de éxito y limpia los campos
    mensaje.value = 'Cliente registrado correctamente'
    nombre.value = ''
    email.value = ''
    password.value = ''
  } catch (err) {
    // Si ocurre un error, muestra el mensaje de error recibido (o uno genérico)
    mensaje.value = err.response?.data?.message || 'Error al registrar'
  }
}
</script>

<style scoped>
.crear-cliente-container {
  min-height: 80vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background: #f6f8fa;
}

.crear-cliente-form {
  background: #fff;
  padding: 2.5rem 2rem;
  border-radius: 12px;
  box-shadow: 0 4px 24px rgba(0,0,0,0.08);
  width: 100%;
  max-width: 350px;
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
}

h2 {
  text-align: center;
  color: #222;
  margin-bottom: 1rem;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 0.3rem;
}

label {
  font-size: 0.98rem;
  color: #444;
  font-weight: 500;
}

input {
  padding: 0.6rem 0.8rem;
  border: 1px solid #d1d5db;
  border-radius: 6px;
  font-size: 1rem;
  transition: border 0.2s;
}

input:focus {
  border: 1.5px solid #007bff;
  outline: none;
}

.btn-registrar {
  background: #007bff;
  color: #fff;
  border: none;
  padding: 0.7rem 0;
  border-radius: 6px;
  font-size: 1.1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.2s;
}

.btn-registrar:hover {
  background: #0056b3;
}

.mensaje {
  margin-top: 0.8rem;
  text-align: center;
  color: #007bff;
  font-weight: 500;
}
</style>