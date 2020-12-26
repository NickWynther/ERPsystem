<template>
  <div>
    <ProductItem v-for="product in allProducts" :key="product.id" :value="product"></ProductItem>
  </div>
</template>

<script>
import {mapGetters, mapActions} from 'vuex'
import ProductItem from '@/components/ProductItem'
export default {
    components:{
        ProductItem
    },
    computed: mapGetters(["allProducts"]),
    methods: mapActions(['fetchProducts']),

    async mounted(){
        //pass category
        this.fetchProducts();
    }
    
}
</script>