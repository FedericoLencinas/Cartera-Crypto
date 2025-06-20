<template>
    <ClienteNavBar></ClienteNavBar>

  <div class="crear-cliente-container">
    <form class="crear-cliente-form" @submit.prevent="registrarCliente">
      <h2>Crear Cliente</h2>
      <div class="form-group">
        <label for="nombre">Nombre</label>
        <input v-model="nombre" id="nombre" placeholder="Nombre" required />
      </div>
      <div class="form-group">
        <label for="email">Email</label>
        <input v-model="email" id="email" placeholder="Email" type="email" required />
      </div>
      <div class="form-group">
        <label for="password">Contraseña</label>
        <input v-model="password" id="password" placeholder="Contraseña" type="password" required />
      </div>
      <button type="submit" class="btn-registrar">Registrar</button>
      <div v-if="mensaje" class="mensaje">{{ mensaje }}</div>
    </form>
  </div>
</template>

<script setup>
import ClienteNavBar from './ClienteNavBar.vue';
import { ref } from 'vue'
import axios from 'axios'

const nombre = ref('')
const email = ref('')
const password = ref('')
const mensaje = ref('')

const registrarCliente = async () => {
  try {
    const res = await axios.post('https://localhost:7273/api/Cliente/register', {
      name: nombre.value,
      email: email.value,
      password: password.value
    })
    mensaje.value = 'Cliente registrado correctamente'
    nombre.value = ''
    email.value = ''
    password.value = ''
  } catch (err) {
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