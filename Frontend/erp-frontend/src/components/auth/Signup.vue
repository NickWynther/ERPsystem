<template>
<form  @submit.prevent="submitSignup">
    <div class="container">
    <h1>Sign Up</h1>
    <p>Please fill in this form to create an account.</p>
    <hr>

    <label ><b>First Name</b></label>
    <input type="text" placeholder="Enter first name" name="firstname" required v-model="firstname">

    <label ><b>Last Name</b></label>
    <input type="text" placeholder="Enter last name" name="lastname" required v-model="lastname">


    <label ><b>Email</b></label>
    <input type="text" placeholder="Enter Email" name="email" required v-model="email">

    <label for="psw"><b>Password</b></label>
    <input type="password" placeholder="Enter Password" name="psw" required v-model="password">

    <label for="psw-repeat"><b>Repeat Password</b></label>
    <input type="password" placeholder="Repeat Password" name="psw-repeat" required v-model="confirmPassword">

    <!--
    <label>
      <input type="checkbox" checked="checked" name="remember" style="margin-bottom:15px"> Remember me
    </label>
    -->

    <button type="submit" >Sign Up</button>
    </div>
</form>
</template>

<script>
import { mapActions} from 'vuex'
export default {
     data() {
        return {
            firstname: "",
            lastname: "",
            email: "",
            password: "",
            confirmPassword: "",
        }
    },

    methods:{
        ...mapActions(['signup']),
        submitSignup(){
            const data = {
                firstname:this.firstname,
                lastname:this.lastname,
                email:this.email,
                password:this.password,
                confirmpassword:this.confirmPassword
            }
            console.log("submitSignup" , data)
            this.signup(data)
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