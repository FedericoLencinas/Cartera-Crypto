<template>
  <div class="navBar">
    <ul>
      <li class="dropdown">
        <span class="dropbtn">Transacciones ▼</span>
        <div class="dropdown-content">
          <RouterLink to="/transacciones/new" class="no-subrayado">Nueva Transacción</RouterLink>
          <RouterLink to="/transacciones/list" class="no-subrayado">Historial de Transacciones</RouterLink>
        </div>
      </li>
    </ul>
  </div>
  <div>
    <RouterView></RouterView>
  </div>
</template>

<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<style>
.navBar {
  background: #333;
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
  position: relative;
  display: inline-block;
}

/* Dropdown styles */
.dropdown .dropbtn {
  color: #fff;
  background: none;
  border: none;
  font-size: 1.1rem;
  padding: 0.5rem 1.2rem;
  border-radius: 6px;
  cursor: pointer;
  font-weight: 500;
  letter-spacing: 1px;
  transition: background 0.2s, color 0.2s;
  user-select: none;
  display: block;
}

.dropdown-content {
  display: none;
  position: absolute;
  background-color: #2f4858;
  min-width: 180px;
  box-shadow: 0 8px 16px rgba(44,62,80,0.15);
  border-radius: 0 0 10px 10px;
  z-index: 1;
  top: 100%;
  left: 0;
}

.dropdown-content .no-subrayado {
  color: #fff;
  padding: 0.7rem 1.2rem;
  display: block;
  text-decoration: none;
  border-radius: 0;
  transition: background 0.2s, color 0.2s;
  font-size: 1rem;
}

.dropdown-content .no-subrayado:hover, 
.dropdown-content .no-subrayado.router-link-exact-active {
  background-color: #f6ae2d;
  color: #2f4858;
  font-weight: 600;
}

/* Show dropdown on hover */
.dropdown:hover .dropdown-content {
  display: block;
}
.dropdown:hover .dropbtn {
  background: #f6ae2d;
  color: #2f4858;
  font-weight: 600;
}

/* General link styles */
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
  background: #f6ae2d;
  color: #2f4858;
  text-decoration: none;
}
</style>