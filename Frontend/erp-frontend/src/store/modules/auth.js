import axios from "axios";

export default{
    actions:{
        async signup(ctx , signupData){
            const url = "https://localhost:44330/api/account/register/user"
            axios.post(url , signupData )
            .then(response =>{ 
                    ctx.commit('saveCreatedUser', response.data)
                    ctx.commit('saveEmail', response.data.email)
                }
            )
        },

        async signin(ctx , signinData){
            console.log("auth signin")
            const url = "https://localhost:44330/api/account/login"
            axios.post(url , signinData )
            .then(response =>{ 
                    ctx.commit('saveEmail', signinData.email)
                    ctx.commit('saveSignInUser', response.data)
                }
            )
        }
    },
    mutations:{
        saveEmail(state, email){
            state.email = email
        },

        saveCreatedUser(state, newUser){
            console.log("User created!!! " , newUser)
        },

        saveSignInUser(state, loginData){
            console.log("User logged in!!! " , loginData)
            state.token = loginData.token
            state.expiration = loginData.expiration
            state.roles = loginData.roles
        },
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
        }
    }
}