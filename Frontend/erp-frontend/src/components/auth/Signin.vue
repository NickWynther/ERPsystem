<template>
<form  @submit.prevent="submitSignin">
    <div class="container">
    <h1>Sign In</h1>
    <p>Please fill in this form to sign in.</p>
    <hr>

    <label ><b>Email</b></label>
    <input type="text" placeholder="Enter Email" name="email" required v-model="email">

    <label for="psw"><b>Password</b></label>
    <input type="password" placeholder="Enter Password" name="psw" required v-model="password">
    <!--
    <label>
      <input type="checkbox" checked="checked" name="remember" style="margin-bottom:15px"> Remember me
    </label>
    -->
    <button type="submit" >Sign In</button>
    </div>
</form>
</template>

<script>
import { mapActions} from 'vuex'
export default {
     data() {
        return {
            email: "",
            password: "",
        }
    },

    methods:{
        ...mapActions(['signin']),
        submitSignin(){
            const data = {
                email:this.email,
                password:this.password,
            }
            console.log("submit.....")
            this.signin(data)
        }
    }
}
</script>

<style scoped>
.container {
  text-align: left;
  margin: 2rem auto;
  max-width: 40rem;
}

input[type=text], input[type=password] {
  width: 100%;
  padding: 15px;
  margin: 5px 0 22px 0;
  display: inline-block;
  border: none;
  background: #f1f1f1;
}

input[type=text]:focus, input[type=password]:focus {
  background-color: #ddd;
  outline: none;
}

hr {
  border: 1px solid #f1f1f1;
  margin-bottom: 25px;
}

/* Set a style for all buttons */
button {
  background-color: #1a037e;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
  opacity: 0.9;
}

button:hover {
  opacity:1;
}
</style>