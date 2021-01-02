import axios from "axios";

export default{
    actions:{
        async fetchCategories(ctx){
            const url = "https://localhost:44306/api/categories"
            axios.get(url)
            .then(response => 
                ctx.commit('updateCategories', response.data)
            )
        }
    },
    mutations:{
        updateCategories(state, categories){
            categories.splice(0,0,{id:0,name:"ALL"})
            state.categories = categories
        },
    },
    state:{
        categories: [],
    },
    getters:{
        allCategories(state){
            return state.categories
        },
    }
}