<template>
    <header>
        <nav>
            <ul>
                <li>{{getEmail}}</li>
                <li><router-link to='/products'>Products</router-link></li>
                <li><router-link to='/authorization'>Authorization</router-link></li>
                <li><router-link to='/signin' @click.native="beforeLogout">{{signinButtonText}}</router-link></li>
                    
            </ul>   
        </nav>
    </header>
</template>

<script>
import {mapGetters , mapMutations} from 'vuex'
export default {
    computed: {
      ...mapGetters(['getEmail' , 'isAuthenticated']),
      signinButtonText(){
        return this.isAuthenticated ? 'logout' : 'signin' ;
      }
    },
    methods:{
      ...mapMutations(['clearSession']),
      beforeLogout(){
        console.log('beforelogout')
        if (this.isAuthenticated){
          console.log("is Authenticated")
          this.clearSession()
        }
      }
    }
}
</script>

<style scoped>
header{
  width: 100%;
  height: 5rem;
  background-color:#11005c;
}

nav{
  height: 100%;
  color: #f1a80a;
}

ul{
  list-style: none;
  margin: 0;
  padding: 0;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

li{
  margin: 0 2rem;
}

a {
    text-decoration: none;
    background: transparent;
    border:1px solid transparent;
    cursor: pointer;
    color:white;
    padding: 0.5rem 1.5rem;
    display: inline-block;
}

a:hover,
a:active,
a.router-link-active{
    color: #f1a80a;
    border-color: #f1a80a;
    background-color: #1a037e;
}

</style>
