<template>
  <v-layout class="mt-2 mb-5">
    <v-flex xs6 sm7 offset-sm2>
      <v-card>
        <v-card-title primary-title>
          <div>
            <h3 class="headline mb-0">Varotojo nustatymai</h3>
          </div>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-layout wrap>
              <v-flex xs12>
                <v-text-field
                  v-model="userObj.userName"
                  label="Prisijungimo vardas"
                  outline
                  readonly
                ></v-text-field>
              </v-flex>
              <v-flex>
                <v-text-field
                  v-model="userObj.name"
                  label="Vardas"
                  :value="userObj.name"
                  outline
                  readonly
                ></v-text-field>
              </v-flex>
              <v-flex>
                <v-text-field
                  v-model="userObj.surname"
                  label="Pavardė"
                  :value="userObj.surname"
                  outline
                  readonly
                ></v-text-field>
              </v-flex>
              <v-flex>
                <v-text-field
                  v-model="userObj.group"
                  label="Grupė"
                  :value="userObj.group"
                  outline
                  readonly
                ></v-text-field>
              </v-flex>
              <v-flex>
                <v-text-field
                  v-model="userObj.studentCode"
                  label="Vidko"
                  :value="userObj.studentCode"
                  outline readonly
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  v-model="userObj.email"
                  label="El. paštas"
                  :value="userObj.email"
                  outline
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field v-model="userObj.uid" label="UID" :value="userObj.uid" outline></v-text-field>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card-text>
        <v-alert class="ml-5 mr-5" :value="erroro" type="error" transition="scale-transition">Įvyko klaida</v-alert>
        <v-alert class="ml-5 mr-5"
          :value="succe"
          type="success"
          transition="scale-transition"
        >Duomenys sėkmingai pakeisti</v-alert>
        <v-card-actions>
          <v-btn flat  @click="submit" color="primary">Pateikti</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import { Device } from "../models/Device";
import { Classroom } from "../models/Classroom";
import { authHeader } from "../components/login/auth-header";
import { RegisteredUsers } from "../models/RegisteredUsers";

export default {
  name: "EditUser",
  props: {
    userObj: Object
  },
  data: () => ({
    RegisteredUsers: null,
    users: [],
    idx: null,
    dialog: false,
    erroro: false,
    succe: false
  }),
  created() {
    this.succe = false;
    this.error = false;
    const currentUser = JSON.parse(localStorage.getItem("user"))["userName"];
    this.$props.userObj = currentUser;
    console.log(this.$props.userObj);
  },
  methods: {
    submit() {
      const headers = { ...authHeader() };
      axios
        .post("api/userlist/postuser/", this.$props.userObj, {
          headers: headers
        })
        .then(response => {
          this.succe = true;
        })
        .catch(error => {
          this.erroro = true;
        });
      this.dialog = false;
    }
  }
};
</script>