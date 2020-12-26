<template>
  <div id="app">
    <h1>ERP system</h1>
    <hr>
    <router-view/>
  </div>
</template>

<script>
//import ProductsList from '@/components/ProductsList'
export default {
  name: 'App',
  components: {
    //ProductsList
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
