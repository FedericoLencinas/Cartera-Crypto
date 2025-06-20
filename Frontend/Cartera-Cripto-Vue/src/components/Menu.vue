<!-- filepath: src/components/Menu.vue -->
<template>
  <nav class="navBar">
    <RouterLink to="/" class="menu-link">Menú</RouterLink>
  </nav>
</template>

<script setup>
import { RouterLink } from 'vue-router'
</script>

<style scoped>
.navBar {
  background: #222;
  padding: 10px 24px;
  display: flex;
  align-items: center;
}
.menu-link {
  background: #007bff;
  color: #fff;
  border: none;
  padding: 10px 28px;
  border-radius: 8px;
  font-size: 1.1rem;
  font-weight: 600;
  text-decoration: none;
  cursor: pointer;
  transition: background 0.2s;
  display: inline-block;
}
.menu-link:hover {
  background: #0056b3;
}
</style>