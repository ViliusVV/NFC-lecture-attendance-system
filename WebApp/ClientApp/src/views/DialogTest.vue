<template>
      <v-dialog v-model="dialog" persistent max-width="600px">   
        <template v-slot:activator="{ on }">    
          <v-btn color="primary" dark v-on="on">Prisijungti</v-btn>
        </template>
        <v-card class="elevation-12">
              <v-toolbar dark color="primary">
                <v-toolbar-title>Prisijungimas</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-tooltip right>
                  <template v-slot:activator="{ on }">
                    <v-btn :href="source" icon large target="_blank" v-on="on">
                      <v-icon large>code</v-icon>
                    </v-btn>
                  </template>
                  <span>Source</span>
                </v-tooltip>
              </v-toolbar>
              <form @submit.prevent="handleSubmit">
                <v-card-text>
                  <v-form>                  
                      <div class="form-group">
                        <!--<div v-show="submitted && !username" class="invalid-feedback">Įveskite vartotojo vardą!</div></-->
                        <v-text-field @keyup.enter="handleSubmit"
                          class="form-control" 
                          :class="{ 'is-invalid': submitted && !username }" 
                          v-model="username" 
                          :error-messages="idErrors"
                          required 
                          prepend-icon="person" 
                          name="username" 
                          label="Vartotojo vardas" 
                          type="text"
                          autofocus
                        ></v-text-field>
                      </div>
                      <div class="form-group">
                        <!--<div v-show="submitted && !password" class="invalid-feedback">Įveskite slaptažodį!</div></-->
                        <v-text-field @keyup.enter="handleSubmit" 
                          v-model="password" 
                          :error-messages="passwordErrors"
                          required 
                          class="form-control" 
                          :class="{ 'is-invalid': submitted && !password }" 
                          id="password" 
                          prepend-icon="lock" 
                          name="password" 
                          label="Slaptažodis" 
                          type="password"
                        ></v-text-field>
                      </div>      
                        <v-alert
                          :value="erroro"
                          type="error"
                          transition="scale-transition"
                        >
                          Neteisingai įvestas vartotojo vardas arba slaptažodis!
                        </v-alert> 
                  </v-form>
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                   <div class="form-group">
                      <button class="btn btn-primary">
                       <v-btn color="primary" @click="handleSubmit">Prisijungti</v-btn>
                       <v-btn color="red" class="white--text" @click="dialog = false">Uždaryti</v-btn>
                    </button>
                   </div>
                </v-card-actions>
              </form>
            </v-card>
      </v-dialog>
</template>

<script>
import { validationMixin } from 'vuelidate'
import { required, minLength, helpers } from 'vuelidate/lib/validators'
import { router } from '../router';
import { userService } from '../services';
//custom validators
function doesPasswordMatch () {
    return true// place holder
  }
function isIdValid () {
    return true// place holder
  }  
export default {
  mixins: [validationMixin],
    validations: {
      username: { required, isIdValid, minLength: minLength(4) },
      password: { required, doesPasswordMatch, minLength: minLength(6) }
    },
    data () {
      return {
        dialog: false,
        notifications: false,
        sound: true,
        widgets: false,
        username: '',
        password: '',
        submitted: false,
        loading: false,
        returnUrl: '',
        returnError: '',
        erroro: false
      }
    },
    computed: {
      idErrors () {
        const errors = []
        if (!this.$v.username.$dirty) return errors
        //only prints error message, doesn't do actual validation
        //!this.$v.user.isIdValid && errors.push('Tokio vartotojo nėra')
        !this.$v.username.minLength && errors.push('Vartotojo vardas turi būti bent iš 4 simbolių')
        !this.$v.username.required && errors.push('Privalomas laukas')
        return errors
      },
      passwordErrors () {
        const errors = []
        if (!this.$v.password.$dirty) return errors
        //!this.$v.password.doesPasswordMatch && errors.push('Neteisingas slaptažodis')
        !this.$v.password.minLength && errors.push('Slaptažodis turi būti bent iš 6 simbolių')
        !this.$v.password.required && errors.push('Privalomas laukas')
        return errors
      }
    },
    created () {
      // reset login status
      //userService.logout();

      // get return url from route parameters or default to '/'
      this.returnUrl = this.$route.query.returnUrl || '/home';
    },
    methods: {
      handleSubmit (e) {
        this.$v.$touch()
        this.submitted = true;
        const { username, password } = this;

        // stop here if form is invalid
        if (this.$v.$invalid) {
            return;
        }

        this.loading = true;
        userService.login(username, password)
          .then(
              user => {
                router.push(this.returnUrl);
                this.dialog = false;
              },
              returnError => {
                  this.returnError = returnError;
                  this.erroro = true;
                  this.loading = false;
              }
          );
      },
      submit () {
        console.log("Submited login");
        //validate form
        this.$v.$touch()
        //if form is valid
        if(!this.$v.$invalid){
          this.dialog = false
        }
      }
    }
  }
</script>