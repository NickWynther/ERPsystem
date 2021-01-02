import axios from "axios";
import router from '@/router'

export default{
    actions:{
        async signup(ctx , signupData){
            const url = "https://localhost:44330/api/account/register/user"
            axios.post(url , signupData )
            .then(response =>{ 
                    ctx.commit('saveEmail', response.data.email)
                    ctx.dispatch('signin' , {email:signupData.email , password:signupData.password})
                    
                }
            )
        },

        async signin(ctx , signinData){
            const url = "https://localhost:44330/api/account/login"
            axios.post(url , signinData )
            .then(response =>{ 
                    ctx.commit('saveEmail', signinData.email)
                    ctx.commit('saveSignInUser', response.data)
                    router.push('/products')
                }
            )
        }
    },
    mutations:{
        saveEmail(state, email){
            state.email = email
        },

        saveSignInUser(state, loginData){
            console.log("User logged in " , loginData.token)
            state.token = loginData.token
            state.expiration = loginData.expiration
            state.roles = loginData.roles
        },

        clearSession(state){
            console.log("clearSession")
            state.token=""
            state.expiration = Date.now()
            state.roles = []
            state.email = "guest"
        }
    },
    state:{
        email: "guest",
        token: "",
        expiration: "",
        roles: []
    },
    getters:{
        getToken(state){
            return state.token
        },
        getEmail(state){
            return state.email
        },

        tokenIsNotExpired(state){
            return Date.parse(state.token) > Date.now()
        },

        isAuthenticated(state){
            return state.token.length > 1
        },

        isAdmin(state){
            return state.roles.includes('admin')
        }
        
    }
}