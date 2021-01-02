<template>
  <li>
    <h3>{{value.name}}</h3>
    <span>Quantity: {{value.quantity}}</span>
    <span>Price: {{value.price}}$</span>
    <button>Add</button>
  </li>
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



li{
  margin:1rem 0;
  box-shadow: 0 2px 8px rgba(0,0,0,0.26);
  border-radius: 12px;
  padding: 1rem;
}

li h3{
  margin: 0.5rem 0;
  font-size: 1.25rem;
  
}

</style>