<template>
  <!-- Contenedor principal de la barra de navegación -->
  <div class="navBar">
    <ul>
      <!-- Enlace al menú principal (Home) -->
      <li>
        <RouterLink to="/" class="no-subrayado">Menu</RouterLink>
      </li>
      <!-- Enlace a la sección de clientes -->
      <li>
        <RouterLink to="/clientes" class="no-subrayado">Clientes</RouterLink>
      </li>
      <!-- Enlace a la sección de transacciones -->
      <li>
        <RouterLink to="/transacciones" class="no-subrayado">Transacciones</RouterLink>
      </li>
      

    </ul>

  </div>

</template>

<script setup>
// Importa RouterLink para la navegación interna de Vue Router
import { RouterLink } from 'vue-router'
</script>

<style scoped>
.navBar {
  background: linear-gradient(90deg, rgb(255, 55, 55) 0%, red 100%);
  padding: 1rem 0;
  box-shadow: 0 2px 8px 0 rgba(44, 62, 80, 0.13);
  border-radius: 0 0 12px 12px;
}

.navBar ul {
  list-style: none;
  display: flex;
  justify-content: center;
  margin: 0;
  padding: 0;
  gap: 2rem;
}

.navBar li {
  display: inline;
}

.no-subrayado {
  color: #fff;
  text-decoration: none;
  font-size: 1.1rem;
  padding: 0.5rem 1.2rem;
  border-radius: 6px;
  transition: background 0.2s, color 0.2s;
  font-weight: 500;
  letter-spacing: 1px;
}

.no-subrayado.router-link-exact-active,
.no-subrayado:hover {
  background: rgb(253, 94, 2);
  color: rgb(253, 253, 253);
  text-decoration: none;
}
</style>