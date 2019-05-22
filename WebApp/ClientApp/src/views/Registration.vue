<template>
  <v-dialog v-model="dialog" persistent max-width="600px">   
    <template v-slot:activator="{ on }">    
      <v-btn color="primary" dark v-on="on">Registruotis</v-btn>
    </template>
    <v-card class="elevation-12">
      <v-toolbar dark color="primary">
        <v-toolbar-title>Užregistruoti</v-toolbar-title>
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
            <form>                  
                <div class="form-group">
                  <!--<div v-show="submitted && !username" class="invalid-feedback">Įveskite vartotojo vardą!</div></-->
                  <v-text-field 
                    class="form-control" :class="{ 'is-invalid': submitted && !username }" 
                    v-model="username" 
                    :error-messages="usernameErrors"
                    required
                    prepend-icon="person" 
                    name="username" 
                    label="Vartotojo prisijungimo vardas" 
                    type="text"
                  ></v-text-field>
                  <!--<div v-show="submitted && !firstName" class="invalid-feedback">Įveskite savo vardą!</div></-->
                  <v-text-field 
                    class="form-control" :class="{ 'is-invalid': submitted && !firstName }" 
                    v-model="firstName"
                    :error-messages="firstNameErrors"
                    required
                    prepend-icon="person_outline" 
                    name="firstName" 
                    label="Studento vardas" 
                    type="text"
                  ></v-text-field>
                  <!--<div v-show="submitted && !lastName" class="invalid-feedback">Įveskite savo pavardę!</div></-->
                  <v-text-field 
                    class="form-control" :class="{ 'is-invalid': submitted && !lastName }" 
                    v-model="lastName"
                    :error-messages="lastNameErrors"
                    required
                    prepend-icon="person_outline" 
                    name="lastName" 
                    label="Studento pavardė" 
                    type="text"
                  ></v-text-field>
                  <!--div v-show="submitted && !grupe" class="invalid-feedback">Įveskite savo akademinę grupę !</div></-->
                  <v-text-field 
                    class="form-control" :class="{ 'is-invalid': submitted && !group }" 
                    v-model="group"
                    prepend-icon="group" 
                    :error-messages="groupErrors"
                    required
                    name="group" 
                    label="Studento akademinė grupė (pvz IFF-7/3)" 
                    type="text"
                  ></v-text-field>
                  <!--<div v-show="submitted && !uid" class="invalid-feedback">Įveskite UID!</div></-->
                  <v-text-field 
                    class="form-control" :class="{ 'is-invalid': submitted && !studentCode }" 
                    v-model="studentCode"
                    :error-messages="studentCodeErrors"
                    required
                    prepend-icon="account_box" 
                    name="studentCode" 
                    label="Vidko kodas" 
                    type="text"
                  ></v-text-field>
                  <v-text-field 
                    class="form-control" :class="{ 'is-invalid': submitted && !uid }" 
                    v-model="uid"
                    :error-messages="uidErrors"
                    required
                    prepend-icon="person_pin" 
                    name="uid" 
                    label="UID (LSP id)" 
                    type="text"
                  ></v-text-field>
                  <!--<div v-show="submitted && !password" class="invalid-feedback">Įveskite slaptažodį!</div></-->
                  <v-text-field 
                    class="form-control" :class="{ 'is-invalid': submitted && !password }" 
                    v-model="password"
                    :error-messages="passwordErrors"
                    required 
                    id="password" 
                    prepend-icon="lock" 
                    name="password" 
                    label="Slaptažodis" 
                    type="password"
                  ></v-text-field>
                  <!--<div v-show="submitted && !verificationPassword" class="invalid-feedback">Įveskite pakartotiną slaptažodį!</div></-->
                  <v-text-field 
                    class="form-control" :class="{ 'is-invalid': submitted && !verificationPassword }" 
                    v-model="verificationPassword" 
                    :error-messages="verificationPasswordErrors"
                    required
                    id="verificationPassword" 
                    prepend-icon="lock" 
                    name="password" 
                    label="Pakartokite Slaptažodį" 
                    type="password"
                  ></v-text-field>
                  <!--<div v-show="submitted && !email" class="invalid-feedback">Įveskite e. paštą!</div></-->
                  <v-text-field 
                    class="form-control" :class="{ 'is-invalid': submitted && !email }" 
                    v-model="email" 
                    :error-messages="emailErrors"
                    required
                    prepend-icon="email" 
                    name="email" 
                    label="Elektroninis paštas" 
                    type="email"
                  ></v-text-field>
                </div>       
            </form>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
              <div class="form-group">
                <button class="btn btn-primary">
                <v-btn color="primary" @click="submit">Registruotis</v-btn>
                <v-btn color="yellow darken-2" class="white--text" @click="clear">Išvalyti</v-btn>
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
  import { required, email, minLength, helpers } from 'vuelidate/lib/validators'
  import { router } from '../router';
  import { userService } from '../services';
  //custom validator for passwords
  function passwordsMustMatch () {
    return this.password === this.verificationPassword // should return Boolean
  }
  export default {
    mixins: [validationMixin],
    validations: {
      username: { required },
      firstName: { required },
      lastName: { required },
      group: { required },
      studentCode: { required },
      uid: { required },
      password: { required, passwordsMustMatch, minLength: minLength(8) },
      verificationPassword: { required, minLength: minLength(8) },
      email: { required, email }
    },
    data () {
      return {
        dialog: false,
        notifications: false,
        sound: true,
        widgets: false,
        username: '',
        firstName: '',
        lastName: '',
        group: '',
        studentCode: '',
        uid: '',
        password: '',
        verificationPassword: '',
        email: '',
        submitted: false,
        loading: false,
        returnUrl: '',
        returnError: '',
      }
    },
    computed: {
      usernameErrors () {
        const errors = []
        if (!this.$v.username.$dirty) return errors
        //only prints error message, doesn't do actual validation
        !this.$v.username.required && errors.push('Privalomas laukas')
        return errors
      },
      firstNameErrors () {
        const errors = []
        if (!this.$v.firstName.$dirty) return errors
        !this.$v.firstName.required && errors.push('Privalomas laukas')
        return errors
      },
      lastNameErrors () {
        const errors = []
        if (!this.$v.lastName.$dirty) return errors
        !this.$v.lastName.required && errors.push('Privalomas laukas')
        return errors
      },
      groupErrors () {
        const errors = []
        if (!this.$v.group.$dirty) return errors
        !this.$v.group.required && errors.push('Privalomas laukas')
        return errors
      },

      studentCodeErrors () {
        const errors = []
        if (!this.$v.studentCode.$dirty) return errors
        !this.$v.studentCode.required && errors.push('Privalomas laukas')
        return errors
      },
      uidErrors () {
        const errors = []
        if (!this.$v.uid.$dirty) return errors
        !this.$v.uid.required && errors.push('Privalomas laukas')
        return errors
      },
      passwordErrors () {
        const errors = []
        if (!this.$v.password.$dirty) return errors
        !this.$v.password.minLength && errors.push('Slaptažodis turi būti iš bent 8 simbolių')
        !(this.password === this.verificationPassword) && errors.push('Neteisingai pakartojote slaptažodį')
        !this.$v.password.required && errors.push('Privalomas laukas')
        return errors
      },
      verificationPasswordErrors () {
        const errors = []
        if (!this.$v.verificationPassword.$dirty) return errors
        !this.$v.verificationPassword.minLength && errors.push('Slaptažodis turi būti iš bent 8 simbolių')
        !(this.password === this.verificationPassword) && errors.push('Neteisingai pakartojote slaptažodį')
        !this.$v.verificationPassword.required && errors.push('Privalomas laukas')
        return errors
      },
      emailErrors () {
        const errors = []
        if (!this.$v.email.$dirty) return errors
        !this.$v.email.email && errors.push('Netinkantis el. paštas')
        !this.$v.email.required && errors.push('Privalomas laukas')
        return errors
      }
    },
    methods: {
      handleSubmit (e) {
        this.submitted = true;
        const { username, password, verificationPassword, uid,
                email, firstName, lastName, group, studentCode } = this;
        if(!this.$v.$invalid){
          //make this work
          this.loading = true;
          userService.register(username, firstName, lastName, group, studentCode,
            uid, email, password)
          .then(
              user => {
                router.push(this.returnUrl);
                this.dialog = false;
              },
              returnError => {
                  this.returnError = returnError;
                  this.loading = false;
              }
          );
        }
        return;
      },
      //when you press the register button
      submit () {
        //validate form
        this.$v.$touch()
        //if form is valid
        if(!this.$v.$invalid){
          const { username, firstName, lastName, group, studentCode, uid,
                password, verificationPassword, email } = this;
          
          //make this work
          this.loading = true;
          userService.register(username, password, firstName, lastName, group, studentCode,
            uid, email )
          .then(
              user => {
                router.push(this.returnUrl);
                this.dialog = false;
              },
              returnError => {
                  this.returnError = returnError;
                  this.loading = false;
              }
          );
        }
        return;
      },
      //clear every data field in the form
      clear () {
        //reset validation
        this.$v.$reset()
        this.firstName = ''
        this.lastName = ''
        this.group = ''
        this.username = ''
        this.studentCode = ''
        this.uid = ''
        this.password = ''
        this.verificationPassword = ''
        this.email = ''
      }
    }
  }
</script>