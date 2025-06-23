<script setup>
// Importaciones necesarias de Vue, vee-validate y otras librerías
import { ref, onMounted, watch } from 'vue'
import { Form, Field, ErrorMessage } from 'vee-validate'
import * as yup from 'yup'
import TransaccionNavBar from './TransaccionNavBar.vue'

// Esquema de validación usando Yup para los campos del formulario
const schema = {
  action: yup.string().required('Debe seleccionar una acción'),
  crypto_code: yup.string().required('Debe seleccionar una criptomoneda'),
  ClienteId: yup.number().typeError('Debe seleccionar un cliente').required('Debe seleccionar un cliente'),
  crypto_amount: yup.number().min(0.00001, 'Debe ser mayor a 0').required('Debe ingresar un monto'),
  money: yup.number().positive().required(),
  datetime: yup.date().required(),
}

// Objeto reactivo para almacenar los datos de la nueva transacción
const newTransaction = ref({
  action: '',
  crypto_code: '',
  ClienteId: '',
  crypto_amount: '',
  money: '',
  datetime: ''
})


// Array reactivo para la lista de clientes
const clients = ref([])
// Función para traer los clientes desde la API
async function traerClientes() {
  let clientApiData = await fetch('https://localhost:7273/api/Cliente')
  clients.value = await clientApiData.json()
}

// Variables y funciones para manejar la fecha y hora actual
const fechaHora = ref('')
function getFechaHoraActual() {
  const x = new Date()
  const anio = x.getFullYear()
  const mes = String(x.getMonth() + 1).padStart(2, '0')
  const dia = String(x.getDate()).padStart(2, '0')
  const horas = String(x.getHours()).padStart(2, '0')
  const minutos = String(x.getMinutes()).padStart(2, '0')
  return `${anio}-${mes}-${dia} ${horas}:${minutos}`
}
function actualizarFechaHora() {
  fechaHora.value = getFechaHoraActual()
  newTransaction.value.datetime = new Date().toISOString()
}
// Al montar el componente, carga los clientes y la hora actual, y actualiza la hora cada minuto
onMounted(() => {
  traerClientes()
  actualizarFechaHora()
  setInterval(actualizarFechaHora, 60000)
})

// Variables reactivas para el valor del dinero y datos de cripto
const money = ref(0)
const crypto = ref('')
// Función para obtener el precio de la criptomoneda según código y cantidad
async function llamarCryptoApi(codigo, cantidad) {
  try {
    const url = `https://criptoya.com/api/${codigo}/ars/${cantidad}`
    const response = await fetch(url)
    const data = await response.json()
    crypto.value = data
    money.value = data.ripio?.ask ?? 0
    // Calcula el total en dinero por la cantidad ingresada
    newTransaction.value.money = money.value * (newTransaction.value.crypto_amount || 0)
  } catch (error) {
    console.error('Error llamando a la API de cripto:', error)
    money.value = 0
    newTransaction.value.money = 0
  }
}
// Observa los cambios en el tipo y cantidad de cripto para actualizar el valor automáticamente
watch(
  () => [newTransaction.value.crypto_code, newTransaction.value.crypto_amount],
  ([codigo, cantidad]) => {
    if (codigo && parseFloat(cantidad) > 0) {
      llamarCryptoApi(codigo, cantidad)
    } else {
      money.value = 0
      newTransaction.value.money = 0
    }
  }
)

// Función para enviar los datos de la transacción a la API
async function enviarDatosApi() {
  // Convierte ClienteId y money a número por si acaso
  newTransaction.value.ClienteId = Number(newTransaction.value.ClienteId)
  
  newTransaction.value.money = Number(money.value) * Number(newTransaction.value.crypto_amount || 0)
  
  newTransaction.value.datetime = new Date().toISOString()

  // Borra campo que no debe enviarse si existe
  delete newTransaction.value.client_id

  // Muestra los datos que se van a enviar (debug)
  console.log(JSON.stringify(newTransaction.value, null, 2))

  // Hace el POST a la API
  let response = await fetch('https://localhost:7273/api/Transaccion', {
    method: 'POST',
    body: JSON.stringify(newTransaction.value),
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer HaciendoElPost'
    }
  })

  // Muestra alertas según el resultado
  if (response.ok) {
    alert('Transacción agregada exitosamente')
  } else {
    const errorData = await response.text()
    console.error('Error en la API:', errorData)
    alert('Error al agregar transacción: ' + errorData)
  }
}
</script>

<template>
  <TransaccionNavBar />
  <div v-if="clients.length === 0">
    <p>Aún no hay clientes cargados.</p>
  </div>
  <div v-else>
    <h1>Nueva Transacción</h1>
    <h3>Hora: {{ fechaHora }}</h3>
    <div class="form-container">
      <Form :validation-schema="schema" @submit="enviarDatosApi" class="form-box label">
        <label>
          Acción:
          <Field as="select" name="action" v-model="newTransaction.action" class="input-field">
            <option disabled value="">Elija una acción</option>
            <option value="purchase">purchase</option>
            <option value="sell">sale</option>
          </Field>
        </label>
        <ErrorMessage name="action" class="error-message" />
        <br />

        <label>
          Cripto:
          <Field as="select" name="crypto_code" v-model="newTransaction.crypto_code" class="input-field">
            <option disabled value="">Seleccione una opción</option>
            <option value="BTC">Bitcoin</option>
            <option value="ETH">Ethereum</option>
            <option value="USDC">USDC</option>
          </Field>
        </label>
        <ErrorMessage name="crypto_code" class="error-message" />
        <br />

        <label>
          Cliente:
          <Field as="select" name="ClienteId" v-model="newTransaction.ClienteId" class="input-field">
            <option disabled value="">Seleccione un cliente</option>
            <option
              v-for="client in clients"
              :key="client.id"
              :value="client.id"
            >{{ client.name }}</option>
          </Field>
        </label>
        <ErrorMessage name="ClienteId" class="error-message" />
        <br />

        <label>
          Monto:
          <Field
            name="crypto_amount"
            type="number"
            step="any"
            v-model="newTransaction.crypto_amount"
            class="input-field"
          />
        </label>
        <ErrorMessage name="crypto_amount" class="error-message" />
        <br /><br />

        
        <Field type="hidden" name="money" v-model="newTransaction.money" />
        <Field type="hidden" name="datetime" v-model="newTransaction.datetime" />

        <h1 v-if="newTransaction.money > 0">
          PRECIO ${{ newTransaction.money.toLocaleString('es-AR') }}
        </h1>
        <input type="submit" value="Guardar" class="submit-button" />
      </Form>
    </div>
  </div>
</template>

<style scoped>
.form-container {
  max-width: 500px;
  margin: 50px auto;
  padding: 2rem;
  background-color: #ffffff;
  border-radius: 12px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}
h1 {
  text-align: center;
  margin-bottom: 2rem;
  font-size: 1.8rem;
  color: #333;
}
.form-box label {
  display: block;
  margin-bottom: 1.5rem;
  font-weight: 600;
  color: #444;
}
.input-field {
  display: block;
  width: 100%;
  padding: 0.6rem;
  margin-top: 0.3rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  font-size: 1rem;
}
.input-field:focus {
  border-color: #007BFF;
  outline: none;
  box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.2);
}
.error-message {
  color: #e74c3c;
  font-size: 0.9rem;
  margin-top: 0.3rem;
}
.submit-button {
  background-color: #007BFF;
  color: white;
  border: none;
  padding: 0.75rem 1.5rem;
  font-size: 1rem;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.3s ease;
  display: block;
  margin: 0 auto;
}
.submit-button:hover {
  background-color: #0056b3;
}
</style>