import axios from "axios";

export default{
    actions:{
        async fetchProducts(ctx){
            const url = "https://localhost:44306/api/products"
            axios.get(url)
            .then(response => 
                ctx.commit('updateProducts', response.data)
            )
        },

        async fetchProductsByCategory(ctx,category){

            if (category.id==0){
                ctx.dispatch("fetchProducts")
                return;
            }

            const url = "https://localhost:44306/api/Products/category/" + category.id
            axios.get(url)
            .then(response => 
                ctx.commit('updateProducts', response.data)
            )
        }
    },
    mutations:{
        updateProducts(state, products){
            state.products = products
        }
    },
    state:{
        products: []
    },
    getters:{
        allProducts(state){
            return state.products
        }
    }
}