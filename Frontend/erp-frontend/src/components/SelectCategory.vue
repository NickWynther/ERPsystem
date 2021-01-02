<template>
    <select v-model="currentCategory">
        <option v-for="category in allCategories" :key="category.id" :value="category">{{category.name}}</option>
    </select>
</template>

<script>
import {mapGetters, mapActions , mapMutations } from 'vuex'
export default {
    data() {
        return {
            currentCategory: {id:0 , name:"ALL"}
        }
    },
    computed: mapGetters(['allCategories'] ),
    methods: {
        ...mapActions(['fetchCategories' , 'fetchProductsByCategory']),
        ...mapMutations(['updateCurrentCategory'])  
    },

    watch:{
        currentCategory: function (){
            this.fetchProductsByCategory(this.currentCategory)
        }
    },

    async mounted(){
        this.fetchCategories();
    }
}
</script>

<style scoped>
select {
  margin:1rem 0;
  box-shadow: 0 2px 8px rgba(0,0,0,0.26);
  border-radius: 10px;
  width: 50%;
  height: 30px;
  outline: none;
  border-color: white;
}
</style>