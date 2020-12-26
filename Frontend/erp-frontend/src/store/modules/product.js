import axios from "axios";

export default{
    actions:{
        async fetchProducts(ctx){
            const url = "https://localhost:44306/api/products"
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