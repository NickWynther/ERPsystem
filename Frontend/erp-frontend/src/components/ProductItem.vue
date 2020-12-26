<template>
  <div>
    <h3>{{value.name}}</h3>
    <span>Quantity: {{value.quantity}}</span>
    <span>Price: {{value.price}}$</span>
    <button>Add</button>
  </div>
</template>

<script>
export default {
    props:{
        value:{
            type: Object,
            required: true
        }
    }
}
</script>

<style scoped>
span{
    margin-right: 1rem;
}
</style>