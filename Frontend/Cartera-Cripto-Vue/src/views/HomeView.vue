<template>
  <div class="container">
    <img
      class="imgLogo"
      src="file:///C:/Users/flenc/Downloads/michael-fortsch-AA5sf7WTv10-unsplash.jpg"
      alt="Crypto"
    />
  </div>
</template>

<style scoped>
.container {
  width: 100vw;
  height: calc(100vh - 60px); /* Ajusta según la altura de tu navbar */
  display: flex;
  justify-content: center;
  align-items: center;
  overflow: hidden;
  margin-top: 60px;
}
.imgLogo {
  width: 100vw;
  height: 100%;
  object-fit: cover;
  object-position: center;
  display: block;
}
</style>