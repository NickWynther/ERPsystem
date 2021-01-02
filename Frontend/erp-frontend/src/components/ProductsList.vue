<template>
<div>
  <SelectCategory/>
  <ul>
    <ProductItem v-for="product in allProducts" :key="product.id" :value="product"></ProductItem>
  </ul>
  <h3 v-if="productListIsEmpty">No items</h3>
</div>
</template>

<script>
import {mapGetters, mapActions} from 'vuex'
import SelectCategory from "@/components/SelectCategory"
import ProductItem from '@/components/ProductItem'
export default {
    components:{
        ProductItem,
        SelectCategory
    },
    computed:{ 
        ...mapGetters(["allProducts"]),
        productListIsEmpty(){
          return this.allProducts.length == 0
        }
    },
    methods: mapActions(['fetchProducts']),

    async mounted(){
        this.fetchProducts();
    }
    
}
</script>

<style scoped>
ul{
  list-style: none;
  margin: 2rem auto;
  max-width: 40rem;
  padding: 0;
}
</style>
